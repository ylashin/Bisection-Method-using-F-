// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.



open NUnit.Framework
open FsUnit

let sign x = 
    if x > 0. then 
        Some(true)
    elif x < 0. then
        Some(false)
    else
        None

let rec bisection (a:float) (b:float) (f: float -> float) (tolerance:float) (maxIter:int) = 

    if maxIter <= 0 then
        failwith "Exceeded max iterations"
    
    if (sign (f a))<>None && (sign (f a)) = (sign (f b) ) then
        failwith "Initial points evaluated function are of same sign"

    
        
    let a' = min a b
    let b' = max a b

   
    let  c = (a' + b') / 2.    
    if (f c) = 0. || (b'-a') < tolerance then
        c
    else                    
        if sign(f c) = sign(f a') then 
            bisection c b' f tolerance (maxIter-1)
        else 
            bisection a' c f tolerance (maxIter-1)
                    

let genRandomNumbers range count =
    let rnd = System.Random()
    List.init count (fun _ -> 
        let sign1 = 
            match (rnd.NextDouble() > 0.5) with
            | true -> 1.
            | false -> -1.

        let sign2 = 
            match (rnd.NextDouble() > 0.5) with
            | true -> 1.
            | false -> -1.

        (sign1 * range * rnd.NextDouble() , sign2 * range * rnd.NextDouble())
    )


let rec findInitialPoints (f: float -> float) range  =    
    let randoms = genRandomNumbers range 1000
    let pair = randoms |> List.tryFind (fun p ->
        let a,b = p 
        if sign (f a) <> None && sign (f a) <> sign (f b) then true
        else    false 
    )

    match pair with
    | Some(a,b) -> pair
    | None -> failwith "Cannot find initial two points"
      
    



type LightBulb(state) =
   member x.On = state
   override x.ToString() =
       match x.On with
       | true  -> "On"
       | false -> "Off"

[<TestFixture>] 
type ``Given a value x`` () =
   [<TestCase(2.,"true")>]
   [<TestCase(-2.,"false")>]
   [<TestCase(0.,"")>]
   member test.``Sign should match expectation`` (x:float,expected)=
            if (expected <> "") then
                sign x = Some(bool.Parse(expected)) |> should be True
            else
                sign x = None |> should be True

[<TestFixture>] 
type ``Given randomization function`` () =
   [<TestCase(10,100)>]
   [<TestCase(50,500)>]
   [<TestCase(500,1000)>]
   member test.``Randome numbers should be in range`` (range:float) count=
    let randoms = genRandomNumbers range count
    randoms.Length = count  |> should be True
    randoms |> Seq.iter (fun pair -> 
        let a,b = pair
        a <= range |> should be True
        a >= -1. * range |> should be True
    )
    
    let negativePair = randoms |> Seq.tryFind (fun pair -> 
        let a,b = pair
        if a < 0. || b < 0. then true
        else false
        )
    1 > 0 |> should be True
    negativePair <> None |> should be True
    
            
        
   

[<EntryPoint>]
let main argv = 

    let func = (fun x -> x**3. - x - 2.)
    let initialPoints = findInitialPoints func 100.
    let a,b = initialPoints.Value
    let root = bisection a b func 0.00001 100
    printfn "Initial Points : %A" initialPoints
    printfn "Root is : %A" root
    printfn "Value of function at root is :%A" ((fun x -> x**3. - x - 2.) root)
    0 // return an integer exit code
