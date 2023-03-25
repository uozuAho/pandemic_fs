namespace pandemic

module PandemicDomain =
    type Role =
        | Medic
        | Scientist

    type CityName =
        | Atlanta
        | Chicago
        | Essen
        | London

    type City = {
        name: CityName
        neighbors: CityName list
    }

    type MoveType =
        | Drive

    type MoveAction = Role * MoveType * CityName

    type Player = {
        role: Role
        location: CityName
    }
    type GameState = {
        players: Player list
        currentPlayer: Role
    }

    type AvailableActions = MoveAction list

    type InitGame = unit -> GameState * AvailableActions

    type MovePlayer = GameState -> MoveAction -> GameState * AvailableActions

    type GameApi = {
        initGame: InitGame
    }

module PandemicImpl =
    open PandemicDomain

    let private cities = [
        { name = Atlanta; neighbors = [ Chicago; London ] }
        { name = Chicago; neighbors = [ Atlanta; Essen ] }
        { name = Essen; neighbors = [ Chicago; London ] }
        { name = London; neighbors = [ Atlanta; Essen ] }
    ]

    let currentPlayer game =
        List.find (fun p -> p.role = game.currentPlayer) game.players

    let private setPlayer game role player =
        let players = List.map (fun p -> if p.role = role then player else p) game.players
        { game with players = players }

    let private getAvailableActions game =
        let player = currentPlayer game
        let city = List.find (fun c -> c.name = player.location) cities
        let moves = List.map (fun n -> (Medic, Drive, n)) city.neighbors
        moves

    let private initGame () =
        let state = {
            players = [
                { role = Medic; location = Atlanta }
                { role = Scientist; location = Atlanta }
            ];
            currentPlayer = Medic
        }
        let actions = getAvailableActions state
        state, actions

    let private movePlayer game (role, moveType, city) =
        let player = List.find (fun p -> p.role = role) game.players
        let player = { player with location = city }
        let newState = setPlayer game role player
        let actions = getAvailableActions newState
        newState, actions

    let api = {
        initGame = initGame
    }
