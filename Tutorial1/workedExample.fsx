type CartItem = string

type EmptyState = NoItems
    
type ActiveState = 
    { UnpaidItems : CartItem list }

type PaidForState = 
    { PaidItems : CartItem list
      Payment : decimal }

type Cart = 
    | Empty of EmptyState
    | Active of ActiveState
    | PaidFor of PaidForState

// =========================
// behavior for states
// =========================

// for empty 
let addToEmptyState state item = 
    Cart.Active { UnpaidItems = [ item ] }

// for active
let addToActiveState state item = 
    let newList = item :: state.UnpaidItems
    Cart.Active { state with UnpaidItems = newList }

let removeFromActiveState state itemToRemove = 
    let newList = state.UnpaidItems |> List.filter (fun i -> i <> itemToRemove)
    match newList with
    | [] -> Cart.Empty NoItems
    | _ -> Cart.Active { state with UnpaidItems = newList }

let payForActiveState state amount = 
    Cart.PaidFor { PaidItems = state.UnpaidItems; Payment = amount }

// state binding
type EmptyState with
    member this.Add = addToEmptyState this

type ActiveState with 
    member this.Add = addToActiveState this
    member this.Remove = removeFromActiveState this
    member this.Pay = payForActiveState this

// =======================
// behavior for Carts
// =======================

let addToCart cart item =
    match cart with
    | Empty state -> state.Add item
    | Active state -> state.Add item
    | PaidFor _ -> 
        printfn "ERROR!! the cart is paid for"
        cart

let removeItemFromCart cart item =
    match cart with
    | Active state -> state.Remove item
    | Empty _ -> 
        printfn "ERROR!! the cart is empty"
        cart
    | PaidFor _ -> 
        printfn "ERROR!! the cart is paid for"
        cart

let displayCart cart =
    match cart with
    | Empty _ -> printfn "The cart is empty"
    | Active state -> printfn "The cart contains %A unpaid items" state.UnpaidItems
    | PaidFor state -> printfn "The cart contains %A paid items. Amount paid %f" state.PaidItems state.Payment

// bindings
type Cart with
    static member NewCart = Cart.Empty NoItems
    member this.Add = addToCart this
    member this.Remove = removeItemFromCart this
    member this.Display = displayCart this

// =================
// Tests
// =================

let emptyCart = Cart.NewCart
printf "emptyCart="; emptyCart.Display

let cartA = emptyCart.Add "A"
printf "cartA="; cartA.Display

let cartAB = cartA.Add "B"
printf "cartAB="; cartAB.Display

let cartB = cartAB.Remove "A"
printf "cartB="; cartB.Display

let emptyCart2 = cartB.Remove "B"
printf "emptyCart2="; emptyCart2.Display

let emptyCart3 = emptyCart2.Remove "B"    //error
printf "emptyCart3="; emptyCart3.Display

//  try to pay for cartA
let cartAPaid = 
    match cartA with
    | Empty _ | PaidFor _ -> cartA 
    | Active state -> state.Pay 100m
printf "cartAPaid="; cartAPaid.Display

//  try to pay for emptyCart
let emptyCartPaid = 
    match emptyCart with
    | Empty _ | PaidFor _ -> emptyCart
    | Active state -> state.Pay 100m
printf "emptyCartPaid="; emptyCartPaid.Display

