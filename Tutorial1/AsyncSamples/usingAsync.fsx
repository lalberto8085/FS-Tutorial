open System

let timerWithAsync =
    let timer = new System.Timers.Timer(2000.0)
    let event = Async.AwaitEvent (timer.Elapsed) |> Async.Ignore
    
    printfn "waiting for timer to start at %O" DateTime.Now.TimeOfDay
    timer.Start()

    printfn "main thread running"

    Async.RunSynchronously event

    printfn "event raised again at %O" DateTime.Now.TimeOfDay

