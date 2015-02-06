open System.IO

let fileWriteWithAsync =
    use stream = new FileStream(@"D:\temp.txt", FileMode.Create)

    printfn "starting async file writing"
    let asyncResult = stream.BeginWrite( Array.empty, 0, 0, null, null)

    let async = Async.AwaitIAsyncResult(asyncResult) |> Async.Ignore

    printfn "doing something in the mean time"

    Async.RunSynchronously async

    printfn "Async write finished"