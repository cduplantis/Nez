module Example


// https://fsharpforfunandprofit.com/posts/designing-with-types-single-case-dus/

type PersonalName =
    { FirstName: string
      MiddleInitial: string option
      LastName: string }

type EmailAddress = EmailAddress of string

type ZipCode = ZipCode of string
type StateCode = StateCode of string

let CreateStateCode (s: string) =
    let s' = s.ToUpperInvariant()
    let otherCode = [ "LA"; "AL"; "MS" ]
    let stateCodes = [ "LA"; "AL"; "MS" ]

    if stateCodes |> List.contains s' then
        Some(StateCode s)
    else
        None

type State = { Description: string; Code: string }

// let States = [
//     State "Alabama" "AL";
//     State ("Alaska" "AK");
//     State ("Arizona" "AZ");
//     State ("Arkansas" "AR");
//     State ("California" "CA");
//     State ("Colorado" "CO");
//     State ("Connecticut" "CT");
//     State ("Delaware" "DE");
//     State ("District" "DC");
//     State ("Florida" "FL");
//     State ("Georgia" "GA");
//     State ("Hawaii" "HI");
//     State ("Idaho" "ID");
//     State ("Illinois" "IL");
//     State ("Indiana" "IN");
//     State ("Iowa" "IA");
//     State ("Kansas" "KS");
//     State ("Louisiana" "LA");
//     State ("Maine" "ME");
//     State ("Maryland" "MD");
//     State ("Massachusetts" "MA");
//     State ("Michigan" "MI");
//     State ("Minnesota" "MN");
//     State ("Mississippi" "MS");
//     State ("Missouri" "MO");
//     State ("Montana" "MT");
//     State ("Nebraska" "NE");
//     State ("Nevada" "NV");
//     State ("New Hampshire" "NH");
//     State ("New Jersey" "NJ");
//     State ("New Mexico" "NM");
//     State ("New York" "NY");
//     State ("North Carolina" "NC");
//     State ("North Dakota" "ND");
//     State ("Ohio" "OH");
//     State ("Oklahoma" "OK");
//     State ("Oregon" "OR");
//     State ("Pennsylvania" "PA");
//     State ("Rhode" "RI");
//     State ("South Carolina" "SC");
//     State ("South Dakota" "SD");
//     State ("Tennessee" "TN");
//     State ("Texas" "TX");
//     State ("Utah" "UT");
//     State ("Vermont" "VT");
//     State ("Virginia" "VA");
//     State ("Washington" "WA");
//     State ("West Virginia" "WV");
//     State ("Wisconsin" "WI");
//     State ("Wyoming" "WY");
//     ]

type PostalAddress =
    { Address1: string
      Address2: string
      City: string
      State: StateCode
      Zip: ZipCode }

type PostalContactInfo =
    { Address: PostalAddress
      IsAddressValid: bool }

type Contact =
    { Name: PersonalName
      EmailAddress: EmailAddress
      IsEmailVerified: bool }

let CreateEmailAddress (s: string) =
    if System.Text.RegularExpressions.Regex.IsMatch(s, @"^\S+@\S+\.\S+$") then
        Some(EmailAddress s)
    else
        None
