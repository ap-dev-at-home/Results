# Results - Result and Exception Handling

**Current Status**: In Development/Trial Phase - final behaviour not determined yet

**Goal**: Simplify result and exception handling using chainable method calls.

### Result - API

| Method | Parameter | Return |
|----------|----------|----------|
| Result.Ok() <br> *- static method*| | A successful Result |
| Result.Ok(T `value`) <br> *- static method*| `value` - the value contained by the result | A successful Result of value type T |
| Result.Fail(string `message`) <br> *- static method*| `message` - the error message | A failed Result |
| Result.Fail(Error `error`) <br> *- static method*| `error` - the error | A failed Result |
| Result.Fail() <br> *- static method*| | A failed Result |
| Result.NotNull(T? `value`, string `message`) <br> *- static method* | `value`- a result value<br><br>`message` - the error message, set if `value` is null | If `value` is not null - A successful Result containing the value <br><br> If `value` is null - A failed Result containing error `message` |
| Result.Handover(params object?[] `value`) <br> *- static method* | `value` - array of obejcts to pass over to the next **Then** call. <br><br> Results will be unwrapped to their containing value before passed over to the next **Then** call<br><br> Any other object will simply be passed over | A Handover ResultCollection |
| Result.`Value` | Property of genric type TValue | The results internal value |
| Result.`Success` | Property of type Bool | The result status |
| Result.`Failed` | Property of type Bool | The result inverted status |
| Result.`Error`| Property of type Error | The error |
| Result.`Logs`| Property of type List\<LogEntry> | The logs |

### Modifications - API
| Method | Parameter | Return |
|----------|----------|----------|
|Result\<TValue>.WhenNull(TValue `value`)| `value` - value to set if the current result value is null<br>Sets the result success status to true <br><br> No effect on a failed result. | The Result (itself) |
|Result\<TValue>.Assert(`Func<TValue, bool>`, string `message`)| `Func` - function receiving the value, returning a bool expression <br><br>If the expression evaluates to false the result will be set to fail - `message` will be set <br><br> No effect on a failed result. | The Result (itself) |

### Execute - API
| Method | Parameter | Return |
|----------|----------|----------|
| Result.Do(`Func<T1..T4, Result<TResult>>`) <br> *- static method* | `Func` - function to be called<br>Optional - up to 4 Parameter| A `Result` of type `TResult` returned from `Func` |
| Result.DoAsync(`Func<T1..T4, Result<TResult>>`) <br> *- static method* | `Func` - function to be called<br>Optional - up to 4 Parameter| A `Task<Result>` of type `TResult` |
| Result.DoInterlocked(`Func<Result<TResult>>`, `object`, `wait`) <br> *- static method* | `Func` - function to be called<br><br>`object` - object to acquire the lock on<br><br>`wait` - optional (default true) - if false the method will immediately return if the lock can not be aquired| A `Result` of type `TResult` returned from `Func` <br><br> A failed `Result` with `Error` of type `InterlockError`, if the lock can not be acquired. |
| Result.Try(`Func<T1..T4, Result<TResult>>`, `Action<Exception>?`) <br> *- static method* | `Func` - function to be called <br> Surrounded by a try catch<br>Optional - up to 4 Parameter<br><br>`Action` - Optional - called on exception<br>The Exception is passed| A `Result` of type `TResult` returned from `Func`<br><br>If an exception occurs - A failed `Result` containing the exception |
| Result.TryAsync(`Func<T1..T4, Result<TResult>>`, `Action<Exception>?`) <br> *- static method* | `Func` - function to be called <br> Surrounded by a try catch<br>Optional - up to 4 Parameter<br><br>`Action` - Optional - called on exception<br>The Exception is passed | A `Task<Result>` of type `TResult` <br><br>If an exception occurs - A failed `Result` containing the exception |
| Result.Try(`Action<T1..T4>`, `Action<Exception>?`) <br> *- static method* | `Action` - action to be called <br> Surrounded by a try catch<br>Optional - up to 4 Parameter<br><br>`Action` - Optional - called on exception<br>The Exception is passed| A success `Result` <br><br>If an exception occurs - A failed `Result` containing the exception |
| Result\<TResult>.Then(`Func<T1..T4, TResult>`)| `Func` - function to be called <br> Up to 4 handover parameters<br><br>If a failed Result is passed from the previous call - **Then** will not be called<br><br>If a Handover object is passed from the previous call containing any failed result - **Then** will not be called | The Result returned from Func |
| Result.FailFast(params `Func<Result>[]`) <br> *- static method* | `Func[]` - functions to be called <br><br> Every call must return a Result. <br><br> A failing call will stop further calls.| Success ResultCollection - if no call failed <br><br> Failed ResultCollection - if any call failed |
| Result.FailSafe(params `Func<Result>[]`) <br> *- static method* | `Func[]` - functions to be called <br><br> Every call must return a Result. <br><br> A failing call will not stop further calls.| Success ResultCollection - if no call failed <br><br> Failed ResultCollection - if any call failed |
| More to come - Development in Progress...|||

### How To Use

```C#
using Results;
using System.Text.Json;
using Util.Logging;

Console.SetWindowSize(200, 80);

Dictionary<int, Person> persons = new()
{
    { 1, new Person("John", "Doe", 15) },
    { 2, new Person("Jane", "Doe", 25) },
    { 3, new Person("Jack", "Doe", 35) }
};

Dictionary<int, Animal> animals = new()
{
    { 1, new Animal("Dog", "Mammal", 5) },
    { 2, new Animal("Cat", "Mammal", 3) }
};

L.Info("Simple call----------------------------------------------");

var r0 = Result.Do(() =>
    {
        const int id = 3;         //existing id

        var person = persons[id];
        
        return Result.Ok(person); //return person as result value
    });

JsonOut(r0); //successful result, contains person
Assert(r0.Success == true && r0.Value != null);


L.NewLine();
L.Info("Simple checked call success------------------------------");

var r1 = Result.Do(() =>
    {
        const int id = 1;                           //existing id

        var person = persons.GetValueOrDefault(id); //get person or null

        return Result.NotNull(person, $"Person ({id}) not found"); //return success result when person is not null
    });

JsonOut(r1); //successful because person is not null
Assert(r1.Success == true && r1.Value != null);


L.NewLine();
L.Info("Simple checked call fail---------------------------------");

var r2 = Result.Do(() =>
    {
        const int id = 4;                           //non existing id

        var person = persons.GetValueOrDefault(id); //get person or null

        return Result.NotNull(person, $"Person ({id}) not found"); //return success result when person is not null
    });

JsonOut(r2); //failed because person was null
Assert(r2.Success == false && r2.Error != null && r2.Error.Message == "Person (4) not found");


L.NewLine();
L.Info("Simple Try with exception-------------------------------");

var r3a = Result.Try(() =>
    {
        const int id = 4;           //non existing id

        var person = persons[id];   //will throw an exception

        return Result.Ok(person);
    });

JsonOut(r3a); //failed result, due to exception, containing 1 Error, the exception
Assert(r3a.Success == false && r3a.Error != null && r3a.Error is ExceptionError && ((ExceptionError)r3a.Error).Exception != null);


L.NewLine();
L.Info("Simple call WhenNull on not null-------------------------");

var r4 = Result.Do(() =>
    {
        const int id = 1;                             //existing id

        var person = persons.GetValueOrDefault(id);   //person with id 1

        return Result.Ok(person).WhenNull(new("Jill", "Doe", 45));
    });

JsonOut(r4); //successful result, containing person with id 1
Assert(r4.Success == true && r4.Value != null && r4.Value.Name == "John");


L.NewLine();
L.Info("Simple call WhenNull on null-----------------------------");

var r5 = Result.Do(() =>
    {
        const int id = 4;                             //non existing id

        var person = persons.GetValueOrDefault(id);   //null

        return Result.Ok(person).WhenNull(new("Jill", "Doe", 45));
    });

JsonOut(r5); //successful result, containing new person
Assert(r5.Success == true && r5.Value != null && r5.Value.Name == "Jill");


L.NewLine();
L.Info("Handover values-----------------------------------------");

var r6 = Result.Do(() =>
    {
        const int id = 1;

        var person = persons[id]; //existing person
        var animal = animals[id]; //existing animal

        return Result.Handover(person, animal); //collect all values
    })
    .Then((Person person, Animal animal) =>
    {
        return Result.Ok(new Owner(person, animal));
    });

JsonOut(r6); //successful result, containing an owner record with person and animal
Assert(r6.Success == true && r6.Value != null && r6.Value.Person.Name == "John" && r6.Value.Animal != null && r6.Value.Animal.Name == "Dog");


L.NewLine();
L.Info("Handover results------------------------------------------");

var r7 = Result.Do(() =>
    {
        const int id = 1;

        var person = Result.Ok(persons[id]); //existing person
        var animal = Result.Ok(animals[id]); //existing animal

        return Result.Handover(person, animal); //collect all results
    })
    .Then((Person person, Animal animal) => //Unwrapped values
    {
        return Result.Ok(new Owner(person, animal));
    });

JsonOut(r7); //successful result, containing an owner record with person and animal
Assert(r7.Success == true && r7.Value != null && r7.Value.Person.Name == "John" && r7.Value.Animal != null && r7.Value.Animal.Name == "Dog");


L.NewLine();
L.Info("Handover with failed results-----------------------------");

var r8 = Result.Do(() =>
    {
        const int id = 1;

        var person = Result.Ok(persons[1]);      //existing person
        var animal = Result.NotNull(animals.GetValueOrDefault(3), "Animal {3} not found"); //non existing animal

        return Result.Handover(person, animal); //collect all results
    })
    .Then((Person person, Animal animal) => //Unwrapped values, fail because animal is null
    {
        return Result.Ok(new Owner(person, animal));
    });

JsonOut(r8); //successful result, containing the error
Assert(r8.Success == false && r8.Error != null && r8.Error.Message == "Animal {3} not found");


L.NewLine();
L.Info("Simple Call Fail-----------------------------------------");

bool thenCalled = false;
var r9 = Result.Do(() =>
    {
        var person = Result.NotNull(persons.GetValueOrDefault(4)); //non existing person

        return person;
    })
    .Then(person => //Unwrapped values, then not called because of failed result
    {
        thenCalled = true;
        return Result.Ok(new Owner(null, null));
    });

JsonOut(r9); //failed result
Assert(r9.Success == false && thenCalled == false);

//----------------------------------------------------------------
//----------------------------------------------------------------
Console.ReadLine();

void JsonOut(object o)
{
    L.Info(JsonSerializer.Serialize(o));
}

void Assert(bool condition)
{
    if (!condition)
    {
        L.Error(LogColor.RED, "Failed");
    }
    else
    {
        L.Info(LogColor.GREEN, "Passed");
    }
}

public record class Person(string Name, string Lastname, int Age);
public record class Animal(string Name, string Type, int Age);
public record class Owner(Person Person, Animal? Animal);
```
