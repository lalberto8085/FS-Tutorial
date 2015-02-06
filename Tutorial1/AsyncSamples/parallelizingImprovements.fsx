let topLimit = 2000

let childTask() = 
    for i in [ 1..topLimit ] do
        for j in [ i..topLimit ] do
            do "Hello".Contains("H") |> ignore

let parentTask = 
    childTask
    |> List.replicate 20
    |> List.reduce (>>)

let asyncChildTask = async { return childTask() }

let asyncParentTask = 
    asyncChildTask
    |> List.replicate 20
    |> Async.Parallel

#time 
parentTask()
#time 

#time
asyncParentTask |> Async.RunSynchronously
#time
