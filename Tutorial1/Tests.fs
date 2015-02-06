type Shape =
    | Circle of int
    | Rectangle of int * int
    | Polygon of (int * int) list
    | Point of (int * int)

let draw shape =
    match shape with
    | Circle r -> printfn "circle radius is %d" r
    | Rectangle (a, b) -> printfn "rectangle height is %d and width is %d" a b
    | Polygon points -> printfn "The polygon is made of this points %A" points
    | _ -> printfn "unknown shape"

let circle = Circle(10)
let rect = Rectangle(4,5)
let polygon = Polygon( [(1,1); (2,2); (3,3)])
let point = Point(2,3)

[circle; rect; polygon; point] |> List.iter draw |> printfn "%A"