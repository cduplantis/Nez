namespace Engen

open System

open SDL2

open Engen.DomainTypes

open FSharp.Control.Reactive
open FSharp.Control.Reactive.Builders



module Window =
    let create windowOptions =
        let flags =
            SDL.SDL_WindowFlags.SDL_WINDOW_VULKAN
            ||| match windowOptions.ScreenMode with
                | Fullscreen -> SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN
                | BorderlessWindow -> SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP
                | Windowed -> SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN

        let args =
            (windowOptions.Title,
             SDL.SDL_WINDOWPOS_CENTERED,
             SDL.SDL_WINDOWPOS_CENTERED,
             windowOptions.Size.x,
             windowOptions.Size.y,
             flags)

        let handle = SDL.SDL_CreateWindow args
        let surface = SDL.SDL_UpdateWindowSurface handle
        { Handle = handle; Surface = surface }

module Graphics =
    let create window =
        let renderer =
            SDL.SDL_CreateRenderer(
                window.Handle,
                -1,
                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED
            // ||| SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC
            )

        { Renderer = renderer }


module Game =
    let create (gameOptions: GameOptions) =
        let w =
            Window.create (gameOptions.WindowOptions)

        let flags =
            SDL.SDL_INIT_VIDEO
            ||| SDL.SDL_INIT_TIMER
            ||| SDL.SDL_INIT_GAMECONTROLLER

        let r = SDL.SDL_Init flags

        if r > 0 then
            printfn "Failed to initialize SDL!!"

        printfn "Initialized SDL"

        { GameOptions = gameOptions
          Window = w }


// module Input =
//     let rec generate x =
//         System.Reactive.Linq.Observable.Create (fun obs -> fun _ -> obs.OnNext("xxx") )
        

    // type sdfInputObserver<'T>() =

    //     let mutable stopped = false

    //     abstract Next : value : 'T -> unit

    //     abstract Error : error : exn -> unit

    //     abstract Completed : unit -> unit

    //     interface IObserver<'T> with

    //         member x.OnNext value = 
    //             if not stopped then 
    //                 x.Next value

    //         member x.OnError e = 
    //             if not stopped then 
    //                 stopped <- true
    //                 x.Error e

    //         member x.OnCompleted () = 
    //             if not stopped then 
    //               stopped <- true
    //               x.Completed ()

    // let running = true

    // let x = observe.While
    // // return an async task
    // let task = async {
    //     let mutable e = new SDL.SDL_Event()
    //     while running do
    //         while SDL.SDL_PollEvent(&e) = 1 do
    //             o.OnNext e
    //         o.OnCompleted
    //     }

    // let generate x =
    //     use subject = new Subject<SDL.SDL_Event>()
    //     ()

    // let ``combineLatest calls map function with pairs of latest values`` () =
    //     let result = ResizeArray()

    //     use obs2 = new ObservableBuilder<int>()
    //     let map (x, y) = x + (y / 2)

    //     Observable.combineLatest obs1 obs2
    //     |> Observable.map map
    //     |> Observable.subscribe (result.Add)
    //     |> ignore

// let loop o =
//     while running do
//         while SDL.SDL_PollEvent(&e) = 1 do
//             o.OnNext e
//         o.OnCompleted
// loop subject

// use o = .Publish().RefCount();
