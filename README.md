# Class of '09 Unity

There are 2 things in this repository.

### -> [Class of '09: Puzzle Showdown](README-PuzzleShowdown.md)

The reverse-engineered Unity build of Puzzle Showdown, functional 1:1 with additional
bug fixes. Credits and screenshots.

### -> [Ren'Py Player](README-RenPy.md)

A Ren'Py interpreter written from scratch in C#, which runs the *compiled, shipped* files
of a real Ren'Py game inside Unity, no source, no re-authoring, no export step. Built and
tested against **Class of '09: Flip Side**.

**Work in progress.** It boots, menus, and plays dialogue with voice, sprites and video,
but it does not play through cleanly yet, and the presentation is still rough.

Lives in [`Assets/Scripts/RenPY`](Assets/Scripts/RenPY) with no dependencies in either
direction, so it can be deleted without touching the game project.
Deep-dive: [architecture notes](Assets/Scripts/RenPY/README.md).

---

Between them the plan is one Unity project for the series: Puzzle Showdown running
natively, the Ren'Py titles running on the interpreter.
