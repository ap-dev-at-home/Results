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
|Result\<TValue>.WhenNull(TValue `value`) | `value` - value to set if the current result value is null<br><br> No effect on a failed result. | The Result (itself) |
|Result\<TValue>.Assert(`Func<TValue, bool>`, string `message`)| `Func` - function receiving the value, returning a bool expression <br><br>If the expression evaluates to false the result will be set to fail - `message` will be set <br><br>No effect on a failed result. | The Result (itself) |
|Result\<TValue>.Recover(TValue `value`) | `value` - value to set to a new Result object if the current result is in a failed status <br><br>Only affects a failed result. | The Result (itself) - if it was in a success status<br><br>A new success Result containing `value` - if it was failed status |

### Execute - API
| Method | Parameter | Return |
|----------|----------|----------|
| Result.Do(`Func<Result<TResult>>`) <br> *- static method* | `Func` - function to be called | A `Result` of type `TResult` returned from `Func` |
| Result.DoAsync(`Func<Result<TResult>>`) <br> *- static method* | `Func` - function to be called | A `Task<Result>` of type `TResult` |
| Result.DoInterlocked(`Func<Result<TResult>>`, `object`, `wait`) <br> *- static method* | `Func` - function to be called<br><br>`object` - object to acquire the lock on<br><br>`wait` - optional (default true) - if false the method will immediately return if the lock can not be aquired| A `Result` of type `TResult` returned from `Func` <br><br> A failed `Result` with `Error` of type `InterlockError`, if the lock can not be acquired. |
| Result.Try(`Func<Result<TResult>>`, `Action<Exception>?`) <br> *- static method* | `Func` - function to be called <br> Surrounded by a try catch<br><br>`Action` - Optional - called on exception<br>The Exception is passed| A `Result` of type `TResult` returned from `Func`<br><br>If an exception occurs - A failed `Result` containing the exception |
| Result.TryAsync(`Func<Result<TResult>>`, `Action<Exception>?`) <br> *- static method* | `Func` - function to be called <br> Surrounded by a try catch<br><br>`Action` - Optional - called on exception<br>The Exception is passed | A `Task<Result>` of type `TResult` <br><br>If an exception occurs - A failed `Result` containing the exception |
| Result.Try(`Action`, `Action<Exception>?`) <br> *- static method* | `Action` - action to be called <br> Surrounded by a try catch<br><br>`Action` - Optional - called on exception<br>The Exception is passed| A success `Result` <br><br>If an exception occurs - A failed `Result` containing the exception |
| Result\<TResult>.Then(`Func<T1..T4, TResult>`)| `Func` - function to be called <br> Up to 4 handover parameters<br><br>If a failed Result is passed from the previous call - **Then** will not be called<br><br>If a Handover object is passed from the previous call containing any failed result - **Then** will not be called | The Result returned from Func |
| Result.FailFast(params `Func<Result>[]`) <br> *- static method* | `Func[]` - functions to be called <br><br> Every call must return a Result. <br><br> A failing call will stop further calls.| Success ResultCollection - if no call failed <br><br> Failed ResultCollection - if any call failed |
| Result.FailSafe(params `Func<Result>[]`) <br> *- static method* | `Func[]` - functions to be called <br><br> Every call must return a Result. <br><br> A failing call will not stop further calls.| Success ResultCollection - if no call failed <br><br> Failed ResultCollection - if any call failed |
| More to come - Development in Progress...|||

### ResultsJson - Exception Safe Json
| Method Signature | Description | 
|------------------|-------------| 
| ResultsJson.From\<T>(string jsonString) | Deserializes a JSON string into a Result object. | 
| ResultsJson.Load\<T>(string path) | Deserializes a JSON file into a Result object. | 
| ResultsJson.Save\<T>(string path, T obj) | Serializes an object to a JSON file. | 
| ResultsJson.From\<T>(Stream stream) | Deserializes a JSON stream into a Result object. | 
| ResultsJson.From\<T>(ReadOnlySpan<byte> utf8Json) | Deserializes a JSON byte span into a Result object. |
