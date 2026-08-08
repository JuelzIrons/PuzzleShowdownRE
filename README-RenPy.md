# Ren'Py Player - running a shipped Ren'Py game inside Unity

A **Ren'Py interpreter written from scratch in C#**, living entirely in
[`Assets/Scripts/RenPY`](Assets/Scripts/RenPY). It takes the *compiled, shipped* files of a
real Ren'Py game with no source, no re-authoring, no export step and then plays them in Unity.

The main test subject is **Class of '09: Flip Side**: 1.0 GB of assets, 107 labels, 576 images,
40 screens, and 7,370 blocks of embedded Python.

```
.rpyc  ->  zlib + Python pickle  ->  Ren'Py AST  ->  engine  ->  Unity
```

Nothing is transpiled ahead of time. The game is parsed and interpreted at runtime.

## Status: work in progress, not playable end to end

**Flip Side does not currently play through properly.** It boots, reaches the menu, starts,
and plays dialogue with voice, sprites, music and video. The further in you get, the
more breaks. Expect wrong or missing backdrops, sprites facing the wrong way or arriving
late, audio that keeps playing when it should not, and layout that does not match the
original.

This is a working interpreter, not a finished port. Treat the numbers below as a measure of
the *engine*, not of how the game looks when you press play.

## Why this is not a small thing

A `.rpyc` file is not a script, it is a **pickled Python object graph** of Ren'Py's own AST,
zlib-compressed inside a slot container. Reading it meant building, in plain C#:

- **A Python unpickler** covering protocols 0–4, reconstructing Ren'Py's `__slots__`-based
  node objects (which serialise as `(None, slots_dict)` state tuples).
- **A Python interpreter** - lexer, parser, tree-walking evaluator. Real `def` with
  closures/`*args`/`**kwargs`, `class` with inheritance, `try`/`except`/`finally`,
  comprehensions, f-strings, `%`-formatting, slicing, unpacking, generators.
  This is the load-bearing part: Flip Side's dialogue is **7,141 embedded Python blocks**,
  not `say` statements.
- **The Ren'Py runtime**: labels, jumps/calls, menus, `scene`/`show`/`hide` with layers,
  tags and z-order, ATL animation with Ren'Py's easing warpers, transitions, text tags,
  channel-based audio, video, and the screen-language system that draws the game's menus.

## Made for IL2CPP / Android

No Python runtime, no `Reflection.Emit`, no code generation, no `dynamic`. It is ordinary
C# that survives AOT compilation. Unity's own `DeflateStream` handles zlib; there are no
third-party dependencies.

Ren'Py's script semantics are synchronous, `renpy.say()` does not return until the player
clicks. Rather than distort the interpreter into continuation-passing style, the script runs
on **its own thread**; work that touches Unity is marshalled to the main thread, and blocking
calls park the script thread until the interaction completes.

## Known Issues

- The **layer / transition / ghost system** is the fragile area. Most visual bugs found so
  far have lived there, and more are likely.
- Only the opening few scenes or so of Flip Side has been played. Later scenes are untested.
- The other two Ren'Py titles have never been loaded, but honestly might work better saying they are older and have less assets

Its also worth noting title screens do NOT work, I had to make a custom one for FlipSide
---

<- Back to [the repository overview](README.md) - See also [Puzzle Showdown](README-PuzzleShowdown.md)
