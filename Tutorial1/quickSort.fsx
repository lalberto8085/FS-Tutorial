﻿open System.Collections;

let rec quicksort = function
    | [] -> []
    | first :: rest ->
        let smaller, larger = List.partition ((>=) first) rest
        quicksort smaller @ [first] @ quicksort larger

printfn "%A" (quicksort [1;5;23;18;9;1;3])
