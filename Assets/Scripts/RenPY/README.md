# Ren'Py player for Unity

Runs a real Ren'Py game inside Unity: it reads the game's compiled `.rpyc` files,
interprets the embedded Python, and drives dialogue, images, audio and video
through Unity.

No Python runtime, no reflection, no code generation, everything is plain C#, so
it works under IL2CPP on Android.

## Using it

1. Put an unzipped Ren'Py game under `Assets/StreamingAssets/`. Use the whole
   original game folder, the one containing `game/` (the `.exe` and `lib/` can
   stay, they are ignored). A `<name>.zip` beside it works too.
2. Add the **Ren'Py Player** component to any GameObject in any scene.
3. Set **Game Path** to the folder name under `StreamingAssets` (e.g. `FlipSide`).
4. Press Play.

Nothing else in the scene is needed. The canvas, audio channels, video surface and
EventSystem are all created at runtime.

### Android

`StreamingAssets` lives inside the APK on Android, where directories cannot be
listed and files cannot be opened directly. Two options:

- **Zip**, put `<name>.zip` in `StreamingAssets`. It is extracted to
  `persistentDataPath` on first launch.
- **Folder**, run **Tools > Ren'Py > Generate Manifest for StreamingAssets
  Games** before building. The player copies the listed files out of the APK on
  first launch.

Either way the install happens once; later launches read the extracted copy.

## How it fits together

```
.rpyc file
   │  RpycFile          container: "RENPY RPC2" slot table + zlib
   ▼
pickle bytes
   │  PickleReader      Python pickle protocols 0-4 → PyValues object model
   ▼
object graph
   │  RenPyAstBuilder   → typed statement nodes (Show, Menu, Python, ATL, screens)
   ▼
RenPyScript            indexes labels/images/screens, links the execution chain
   │
   ▼
RenPyEngine            walks the chain; embedded Python runs on PyInterp
   │
   ▼
IRenPyHost             UnityRenPyHost → RenPyUI / RenPyAudio / VideoPlayer
```

### Folders

| Folder     | What lives there                                                        |
|------------|-------------------------------------------------------------------------|
| `Python/`  | Lexer, parser and tree-walking interpreter for the Python subset, plus the shared value model (`PyValues`, `PyOps`) and builtins. No Unity dependency. |
| `Pickle/`  | `.rpyc` container reader and the Python unpickler.                       |
| `Ast/`     | Typed Ren'Py statement/ATL/screen nodes and the builder that produces them. |
| `Runtime/` | The engine, scene state, ATL evaluation, text tags, and the `renpy.*` API. Host-agnostic, so it can be driven headlessly in tests. |
| `Unity/`   | `RenPyPlayer` plus the Unity implementations of display, audio and file access. |
| `Editor/`  | The Android manifest generator.                                          |

The split matters: everything below `Unity/` compiles and runs without Unity,
which is how the engine is regression-tested against a real game.

## Threading

Ren'Py's script semantics are synchronous, `renpy.say()` does not return until
the player clicks. Rewriting the interpreter in continuation-passing style to fit
a coroutine would have distorted it, so instead the script runs on its own thread:

- The engine thread calls `IRenPyHost` methods directly.
- Non-blocking calls (show, hide, play audio) are queued to the main thread.
- Blocking calls (say, menu, pause, movie, transition) queue the UI work and then
  park the engine thread until the main thread reports the interaction finished.

`RenPyPlayer.Update` pumps that queue and owns all interaction timing.

## What is supported

**Statements**, `label`, `jump`, `call`, `return`, `if`/`elif`/`else`, `while`,
`menu`, `scene`, `show`, `hide`, `with`, `image`, `transform`, `define`,
`default`, `python`/`init python`, `say`, `screen`, `style`, plus the built-in
user statements `play`, `queue`, `stop`, `pause`, `voice`, `window`, and
`show`/`hide screen`.

**Python**, expressions, `def` (defaults, `*args`, `**kwargs`, closures),
`class` with inheritance, `for`/`while`/`break`/`continue`, `try`/`except`/
`finally`, `with`, comprehensions (list/set/dict/generator), f-strings,
`%`-formatting and `str.format`, slicing, unpacking, `global`, `import`, and the
common builtins. Both Python 2 and 3 spellings, since Ren'Py 7 games are Python 2.

**Ren'Py API**, `renpy.say/pause/jump/call/return_statement/checkpoint/show/
hide/scene/transition/image/movie_cutscene/variant/loader/random`, the
`renpy.music`/`sound`/`audio` channels, `Character`, `config`, `gui`,
`persistent`, `preferences`, displayables (`Image`, `Solid`, `Text`, `Frame`,
`Composite`, `Movie`, …), positions (`left`, `center`, `truecenter`, …), and the
transition set.

**Text tags**, `{p}`, `{w}`, `{nw}`, `{cps}` are lifted out as timing; `{b}`,
`{i}`, `{u}`, `{color}`, `{size}`, `{alpha}`, `{k}` become TextMeshPro markup.
`[variable]` interpolation is evaluated against the store.

**Audio**, Ren'Py's `<from N to N loop N>` filename clauses, per-channel fades,
pause/resume, and arbitrary game-defined channel names.

## Boot sequence

The player follows Ren'Py's own path (`renpy/common/00start.rpy`):

```
label splashscreen  →  main menu  →  label start
```

A game with no `main_menu` screen or label falls straight through to `start`.
Turn **Boot Through Menu** off to skip to the start label during development.

## Screens

Screen language is evaluated for real: `ScreenEvaluator` walks the SL tree against
a live scope (`for` iterates, `if` branches, `python` runs, `use` splices) and
produces a `ScreenWidget` tree, which `RenPyScreenView` builds into uGUI.

Supported widgets: `imagebutton`, `textbutton`, `button`, `text`, `label`, `add`,
`vbox`/`hbox`/`fixed`/`frame`/`grid`/`viewport`, plus the non-visual `on`, `key`
and `timer`. Buttons carry idle/hover art, `hover_sound`/`activate_sound`, and
`hovered`/`unhovered` actions.

Actions: `Start`, `MainMenu`, `Quit`, `Return`, `Jump`, `Show`/`Hide`, `ShowMenu`,
`Play`, `Stop`, `Function`, `SetField`, `ToggleField`, `SetVariable`,
`ToggleVariable`, `If`, and no-op stubs for the save/load family.

Actions are **queued, not run where clicked**, they can jump, start the game or
call Python, none of which is safe from Unity's thread while the engine owns the
interpreter. The engine drains the queue between statements.

## Persistence

The `persistent` store is saved to **PlayerPrefs** (namespaced by game path) on
quit and on mobile pause, and reloaded before init so `default` statements and
menu conditions see real progress. That is what makes an endings gallery work
across sessions.

Values are type-tagged, so ints, floats, strings, bools, lists, tuples, sets and
dicts all round-trip. Class instances are skipped rather than silently corrupted.

## Known limits

- **Screen layout is approximate.** Widgets are positioned from their transform
  and boxes use Unity layout groups, but Ren'Py's style system (inheritance,
  `style_prefix` lookup, borders/padding from `Frame`) is not implemented, so
  spacing will not match pixel for pixel.
- **No save/load or rollback.** The API calls exist and are no-ops, so scripts
  that call them run, but game state other than `persistent` is not saved.
- **ATL `parallel` runs only its first track**, and `time`/`event` are ignored.
  Ordinary timed steps, warpers and `repeat` do animate.
- `.rpa` archives are not read. Games that pack assets into `.rpa` need extracting
  first; loose-file games (the common case for the ones tested) work as-is.

## Testing

`Python/`, `Pickle/`, `Ast/` and `Runtime/` build outside Unity, which is how they
are verified: a headless `IRenPyHost` runs a real game start to finish and asserts
that no statement errors and no asset lookups fail.
