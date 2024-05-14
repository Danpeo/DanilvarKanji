module Danilvar.MathUtils

let calcPercentage numerator denominator = (numerator / denominator) * 100.0

let rec isHappyNumber number =
  let squareSum =
    number.ToString()
    |> Seq.sumBy (fun c -> (int (c.ToString()) * int (c.ToString())))

  match squareSum with
  | 1 -> true
  | 4 -> false
  | _ -> isHappyNumber squareSum
