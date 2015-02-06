open System

let sleepWorkFlow = async {
    printfn "Started sleeping at %O" DateTime.Now.TimeOfDay
    do! Async.Sleep 2000
    printfn "Finished sleeping at %O" DateTime.Now.TimeOfDay
}

Async.RunSynchronously sleepWorkFlow  

let nestedWorkFlow = async {
    printfn "Starting parent"

    let! childWF = Async.StartChild sleepWorkFlow


}
