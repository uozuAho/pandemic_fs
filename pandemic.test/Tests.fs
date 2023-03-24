module Tests

open Xunit
open pandemic.Game

[<Fact>]
let ``Move player`` () =
    let game = initGame()
    let game2 = movePlayer game (Medic, Drive, Atlanta)
    let player = getPlayer game2 Medic
    Assert.Equal(Atlanta, player.location)