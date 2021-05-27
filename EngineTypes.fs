namespace Engen

open SDL2
open System

[<AutoOpen>]
module DomainTypes =
    type Vector = { x: int; y: int }

    type Position = Vector

    type Size = Vector

    type Color = { r: byte; g: byte; b: byte; a: byte }

    type ScreenMode =
        | Fullscreen
        | BorderlessWindow
        | Windowed

    type WindowOptions =
        { Title: string
          Size: Size
          ScreenMode: ScreenMode }

    type Window =
        { Handle: nativeint
          Surface: int }
        interface System.IDisposable with
            member x.Dispose() =
                SDL.SDL_DestroyWindow x.Handle
                printfn "Closed main window"

    type Graphics =
        { Renderer: nativeint }
        interface System.IDisposable with
            member x.Dispose() =
                SDL.SDL_DestroyRenderer x.Renderer
                printfn "Closed Renderer"

    type GameOptions = 
        { WindowOptions: WindowOptions}

    type Game =
        { GameOptions: GameOptions 
          Window: Window }
        interface IDisposable with
            member this.Dispose() =
                SDL.SDL_Quit()
                printfn "Deinitialized SDL"
