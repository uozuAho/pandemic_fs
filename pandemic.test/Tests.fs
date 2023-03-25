module Tests

open Xunit
open pandemic.PandemicDomain
open pandemic.PandemicImpl

module TestUtils =
    let findPlayer game role =
        game.players |> List.find (fun p -> p.role = role)

open TestUtils

[<Fact>]
let ``drive to neighbour`` () =
    let game, _ = api.initGame()
    let game2, _ = api.movePlayer game (Medic, Drive, Chicago)
    let medic = findPlayer game2 Medic
    Assert.Equal(Chicago, medic.location)
