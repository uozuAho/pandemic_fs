namespace pandemic

module PandemicDomain =
    type Role =
        | Medic
        | Scientist

    type City =
        | Atlanta
        | Chicago
        | Essen
        | London

    type MoveType =
        | Drive

    type MoveAction = Role * MoveType * City

    type Player = {
        role: Role
        location: City
    }
    type GameState = {
        players: Player list
    }

    type InitGame = unit -> GameState

    type MovePlayer = GameState -> MoveAction -> GameState

    type GameApi = {
        initGame: InitGame
        movePlayer: MovePlayer
    }

module PandemicImpl =
    open PandemicDomain

    let private initGame () = {
        players = [
            { role = Medic; location = Atlanta }
            { role = Scientist; location = Atlanta }
        ]
    }

    let private setPlayer game role player =
        let players = List.map (fun p -> if p.role = role then player else p) game.players
        { game with players = players }

    let private movePlayer game (role, moveType, city) =
        let player = List.find (fun p -> p.role = role) game.players
        let player = { player with location = city }
        setPlayer game role player


    let api = {
        initGame = initGame
        movePlayer = movePlayer
    }
