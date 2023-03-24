namespace pandemic

module Game =
    type Role =
        | Medic
        | Scientist
        | Researcher
        | OperationsExpert
        | QuarantineSpecialist
        | ContingencyPlanner
        | Dispatcher
        
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
        // hand: Card list
    }
    type GameState = {
        players: Player list
    }
    let initGame () = {
        players = [
            { role = Medic; location = Atlanta }
            { role = Scientist; location = Atlanta }
        ]
    }
    
    let private setPlayer game role player =
        let players = List.map (fun p -> if p.role = role then player else p) game.players
        { game with players = players }
    
        