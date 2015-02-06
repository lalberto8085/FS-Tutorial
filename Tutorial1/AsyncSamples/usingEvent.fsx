open System

let timerWithCallBack= 
    let event = new System.Threading.AutoResetEvent(false)

    let timer = new System.Timers.Timer(2000.0)
    timer.Elapsed.Add (fun _ -> event.Set() |> ignore)

    printfn "waiting for timer to start at %O" DateTime.Now.TimeOfDay
    timer.Start()

    printfn "main thread running"

    event.WaitOne() |> ignore

    printfn "event raised again at %O" DateTime.Now.TimeOfDay