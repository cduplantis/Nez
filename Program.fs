// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
open System

open SDL2
open Engen


let defaultWindowOptions =
    { Title = "Untitled Engen Game"
      Size = { x = 1280; y = 720 }
      ScreenMode = ScreenMode.Windowed }






type GameState = { Position: Position; Color: Color }

let Init (g: Game) =
    let color : Color = { r = 255uy; g = 0uy; b = 0uy; a = 0uy }

    let state : GameState =
        { Position = { x = 100; y = 100 }
          Color = color }

    state

let rand = Random()

let newPosition (p: Position, max: Position) =
    { x = rand.Next(max.x)
      y = rand.Next(max.y) }

let Update (gs: GameState) =
    { gs with
          Position =
              { gs.Position with
                    x = gs.Position.x + 10
                    y = gs.Position.y + 10 } }

let Render graphics gs =
    let rndrd = graphics.Renderer

    SDL.SDL_SetRenderDrawColor(rndrd, 0uy, 0uy, 0uy, 255uy) |> ignore
    // |> printf "RenderDrawColor: %i"

    let mutable rect = SDL.SDL_Rect()

    SDL.SDL_RenderGetViewport(rndrd, &rect) |> ignore

    SDL.SDL_RenderFillRect(rndrd, &rect) |> ignore

    let r, g, b, a =
        gs.Color.r, gs.Color.g, gs.Color.b, gs.Color.a

    SDL.SDL_SetRenderDrawColor(rndrd, r, g, b, a) |> ignore
    // |> printf "RenderDrawColor: %i"

    SDL.SDL_RenderDrawPoint(rndrd, gs.Position.x, gs.Position.y) |> ignore
    // |> printf "RenderPoint: %i"

    SDL.SDL_RenderPresent rndrd |> ignore
    // g.Window.UpdateSurface  |>  printf "UpdateSurface: %i"
    ()

let rec Run graphics gs =
    let g2 = Update gs
    Render graphics g2
    Run graphics g2

// Define a function to construct a message to print
let from whom = sprintf "from %s" whom



[<EntryPoint>]
let main argv =
    XPlatform.Init
    let message = from "F#" // Call the function

    let options =
        { WindowOptions =
              { defaultWindowOptions with
                    Title = "Main Game" } }

    use g = Game.create options
    use graphics = Graphics.create g.Window
    let gamestate = Init g
    Run graphics gamestate

    printfn "Hello world %s" message

    System.Threading.Thread.Sleep 3000

    0 // return an integer exit code
