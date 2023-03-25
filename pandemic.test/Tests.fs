module Tests

open Xunit
open pandemic.PandemicDomain
open pandemic.PandemicImpl

module TestUtils =
    let findPlayer game role =
        game.players |> List.find (fun p -> p.role = role)

open TestUtils

[<Fact>]
let ``Move player`` () =
    let game = api.initGame()
    let game2 = api.movePlayer game (Medic, Drive, Chicago)
    let medic = findPlayer game2 Medic
    Assert.Equal(Chicago, medic.location)
