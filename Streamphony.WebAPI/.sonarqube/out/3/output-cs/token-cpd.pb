√
MC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.WebAPI\Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
Log 
. 
Logger 

= 
new 
LoggerConfiguration $
($ %
)% &
.		 
MinimumLevel		 
.		 
Override		 
(		 
$str		 &
,		& '
LogEventLevel		( 5
.		5 6
Information		6 A
)		A B
.

 
Enrich

 
.

 
FromLogContext

 
(

 
)

 
. !
CreateBootstrapLogger 
( 
) 
; 
builder 
. 
Services 
. 
AddControllers 
(  
cfg  #
=>$ &
{ 
cfg 
. 
Filters 
. 
Add 
< 
ExceptionFilter #
># $
($ %
)% &
;& '
} 
) 
; 
builder 
. 
Services 
. #
AddEndpointsApiExplorer (
(( )
)) *
;* +
builder 
. 
Services 
. /
#AddDateOnlyTimeOnlyStringConverters 4
(4 5
)5 6
;6 7
builder 
. 
Services 
. 
AddSwaggerGen 
( 
)  
;  !
builder 
. 
Services 
. "
AddApplicationServices '
(' (
builder( /
./ 0
Configuration0 =
)= >
;> ?
var 
app 
= 	
builder
 
. 
Build 
( 
) 
; 
if 
( 
app 
. 
Environment 
. 
IsDevelopment !
(! "
)" #
)# $
{ 
app 
. #
UseSwaggerDocumentation 
(  
)  !
;! "
} 
app 
. 
UseHttpsRedirection 
( 
) 
; 
app   
.   
UseDbTransaction   
(   
)   
;   
app!! 
.!! 
MapControllers!! 
(!! 
)!! 
;!! 
app## 
.## 
Run## 
(## 
)## 	
;##	 
›
gC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.WebAPI\Middlewares\TransactionMiddleware.cs
	namespace 	
Streamphony
 
. 
WebAPI 
. 
Middlewares (
;( )
public 
class !
TransactionMiddleware "
(" #
RequestDelegate# 2
next3 7
)7 8
{ 
private 
readonly 
RequestDelegate $
_next% *
=+ ,
next- 1
;1 2
public		 

async		 
Task		 
InvokeAsync		 !
(		! "
HttpContext		" -
context		. 5
,		5 6
IUnitOfWork		7 B

unitOfWork		C M
)		M N
{

 
CancellationToken 
cancellationToken +
=, -
context. 5
.5 6
RequestAborted6 D
;D E
if 

( 
context 
. 
Request 
. 
Method "
==# %

HttpMethod& 0
.0 1
Get1 4
.4 5
Method5 ;
); <
{ 	
await 
_next 
( 
context 
)  
;  !
return 
; 
} 	
try 
{ 	
await 

unitOfWork 
. !
BeginTransactionAsync 2
(2 3
cancellationToken3 D
)D E
;E F
await 
_next 
( 
context 
)  
;  !
await 

unitOfWork 
. "
CommitTransactionAsync 3
(3 4
cancellationToken4 E
)E F
;F G
} 	
catch 
( 
	Exception 
) 
{ 	
await 

unitOfWork 
. $
RollbackTransactionAsync 5
(5 6
cancellationToken6 G
)G H
;H I
} 	
} 
} Ç
hC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.WebAPI\Middlewares\RequestTimerMiddleware.cs
	namespace 	
Streamphony
 
. 
WebAPI 
. 
Middlewares (
;( )
public 
class "
RequestTimerMiddleware #
(# $
RequestDelegate$ 3
next4 8
,8 9
ILogger: A
<A B"
RequestTimerMiddlewareB X
>X Y
loggerZ `
)` a
{ 
private 
readonly 
RequestDelegate $
_next% *
=+ ,
next- 1
;1 2
private 
readonly 
ILogger 
< "
RequestTimerMiddleware 3
>3 4
_logger5 <
== >
logger? E
;E F
public

 

async

 
Task

 
InvokeAsync

 !
(

! "
HttpContext

" -
context

. 5
)

5 6
{ 
var 
	stopwatch 
= 
	Stopwatch !
.! "
StartNew" *
(* +
)+ ,
;, -
await 
_next 
( 
context 
) 
; 
	stopwatch 
. 
Stop 
( 
) 
; 
_logger 
. 
LogInformation 
( 
$str S
,S T
context 
. 
Request 
? 
. 
Method #
,# $
context 
. 
Request 
? 
. 
Path !
.! "
Value" '
,' (
	stopwatch 
. 
ElapsedMilliseconds )
)) *
;* +
} 
} ¶
dC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.WebAPI\Filters\ValidateModelAttribute.cs
	namespace 	
Streamphony
 
. 
WebAPI 
. 
Filters $
;$ %
public 
class "
ValidateModelAttribute #
:$ %!
ActionFilterAttribute& ;
{ 
public		 

override		 
void		 
OnActionExecuting		 *
(		* +"
ActionExecutingContext		+ A
context		B I
)		I J
{

 
if 

( 
! 
context 
. 

ModelState 
.  
IsValid  '
)' (
{ 	
var 
apiError 
= 
new 
ErrorResponse ,
{ 

StatusCode 
= 
$num  
,  !
StatusPhrase 
= 
$str ,
,, -
	Timestamp 
= 
DateTime $
.$ %
Now% (
} 
; 
foreach 
( 
var 
error 
in !
context" )
.) *

ModelState* 4
)4 5
{ 
foreach 
( 
var 
inner "
in# %
error& +
.+ ,
Value, 1
.1 2
Errors2 8
)8 9
{ 
apiError 
. 
Errors #
.# $
Add$ '
(' (
inner( -
.- .
ErrorMessage. :
): ;
;; <
} 
} 
context 
. 
Result 
= 
new  "
BadRequestObjectResult! 7
(7 8
apiError8 @
)@ A
;A B
} 	
} 
} ä(
]C:\Personal\amdaris\project\streamphony-asp-net\Streamphony.WebAPI\Filters\ExceptionFilter.cs
	namespace 	
Streamphony
 
. 
WebAPI 
. 
Filters $
;$ %
public

 
class

 
ExceptionFilter

 
:

 
IExceptionFilter

 /
{ 
public 

void 
OnException 
( 
ExceptionContext ,
context- 4
)4 5
{ 
var 
response 
= 
new 
ErrorResponse (
{ 	

StatusCode 
= 
( 
int 
) 
HttpStatusCode ,
., -
InternalServerError- @
,@ A
StatusPhrase 
= 
HttpStatusCode )
.) *
InternalServerError* =
.= >
ToString> F
(F G
)G H
,H I
	Timestamp 
= 
DateTime  
.  !
UtcNow! '
} 	
;	 

switch 
( 
context 
. 
	Exception !
)! "
{ 	
case 
ValidationException $
validationException% 8
:8 9
response 
. 

StatusCode #
=$ %
(& '
int' *
)* +
HttpStatusCode+ 9
.9 :

BadRequest: D
;D E
response 
. 
StatusPhrase %
=& '
HttpStatusCode( 6
.6 7

BadRequest7 A
.A B
ToStringB J
(J K
)K L
;L M
response 
. 
Errors 
.  
AddRange  (
(( )
validationException) <
.< =
Errors= C
.C D
SelectD J
(J K
errorK P
=>Q S
errorT Y
.Y Z
ErrorMessageZ f
)f g
)g h
;h i
break 
; 
case 
NotFoundException "
notFoundException# 4
:4 5
response 
. 

StatusCode #
=$ %
(& '
int' *
)* +
HttpStatusCode+ 9
.9 :
NotFound: B
;B C
response 
. 
StatusPhrase %
=& '
HttpStatusCode( 6
.6 7
NotFound7 ?
.? @
ToString@ H
(H I
)I J
;J K
response 
. 
Errors 
.  
Add  #
(# $
notFoundException$ 5
.5 6
Message6 =
)= >
;> ?
break   
;   
case!! 
DuplicateException!! #
duplicateException!!$ 6
:!!6 7
response"" 
."" 

StatusCode"" #
=""$ %
(""& '
int""' *
)""* +
HttpStatusCode""+ 9
.""9 :
Conflict"": B
;""B C
response## 
.## 
StatusPhrase## %
=##& '
HttpStatusCode##( 6
.##6 7
Conflict##7 ?
.##? @
ToString##@ H
(##H I
)##I J
;##J K
response$$ 
.$$ 
Errors$$ 
.$$  
Add$$  #
($$# $
duplicateException$$$ 6
.$$6 7
Message$$7 >
)$$> ?
;$$? @
break%% 
;%% 
case&& !
UnauthorizedException&& &!
unauthorizedException&&' <
:&&< =
response'' 
.'' 

StatusCode'' #
=''$ %
(''& '
int''' *
)''* +
HttpStatusCode''+ 9
.''9 :
Unauthorized'': F
;''F G
response(( 
.(( 
StatusPhrase(( %
=((& '
HttpStatusCode((( 6
.((6 7
Unauthorized((7 C
.((C D
ToString((D L
(((L M
)((M N
;((N O
response)) 
.)) 
Errors)) 
.))  
Add))  #
())# $!
unauthorizedException))$ 9
.))9 :
Message)): A
)))A B
;))B C
break** 
;** 
default++ 
:++ 
response,, 
.,, 
Errors,, 
.,,  
Add,,  #
(,,# $
context,,$ +
.,,+ ,
	Exception,,, 5
.,,5 6
Message,,6 =
),,= >
;,,> ?
break-- 
;-- 
}.. 	
context00 
.00 
Result00 
=00 
new00 
ObjectResult00 )
(00) *
response00* 2
)002 3
{11 	

StatusCode22 
=22 
response22 !
.22! "

StatusCode22" ,
}33 	
;33	 

}44 
}55 ∫)
bC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.WebAPI\Extensions\ServiceExtensions.cs
	namespace 	
Streamphony
 
. 
WebAPI 
. 

Extensions '
;' (
public 
static 
class 
ServiceExtensions %
{ 
public 

static 
IServiceCollection $"
AddApplicationServices% ;
(; <
this< @
IServiceCollectionA S
servicesT \
,\ ]
IConfiguration^ l
configurationm z
)z {
{ 
string 
password 
= 
Environment %
.% &"
GetEnvironmentVariable& <
(< =
$str= U
)U V
!V W
;W X
string 
connectionString 
=  !
configuration" /
./ 0
GetConnectionString0 C
(C D
$strD W
)W X
!X Y
;Y Z
connectionString 
+= 
$" 
$str '
{' (
password( 0
}0 1
$str1 2
"2 3
;3 4
services 
. 
AddDbContext 
<  
ApplicationDbContext 2
>2 3
(3 4
options4 ;
=>< >
options 
. 
UseSqlServer  
(  !
connectionString! 1
)1 2
)2 3
;3 4
services!! 
.!! 

AddMediatR!! 
(!! 
typeof!! "
(!!" #
GetAllUsersHandler!!# 5
)!!5 6
.!!6 7
Assembly!!7 ?
)!!? @
;!!@ A
services$$ 
.$$ 
	AddScoped$$ 
<$$ 
IUnitOfWork$$ &
,$$& '

UnitOfWork$$( 2
>$$2 3
($$3 4
)$$4 5
;$$5 6
services%% 
.%% 
	AddScoped%% 
<%% 
IUserRepository%% *
,%%* +
UserRepository%%, :
>%%: ;
(%%; <
)%%< =
;%%= >
services&& 
.&& 
	AddScoped&& 
<&& 
ISongRepository&& *
,&&* +
SongRepository&&, :
>&&: ;
(&&; <
)&&< =
;&&= >
services'' 
.'' 
	AddScoped'' 
<'' 
IAlbumRepository'' +
,''+ ,
AlbumRepository''- <
>''< =
(''= >
)''> ?
;''? @
services(( 
.(( 
	AddScoped(( 
<(( 
IGenreRepository(( +
,((+ ,
GenreRepository((- <
>((< =
(((= >
)((> ?
;((? @
services)) 
.)) 
	AddScoped)) 
<)) %
IUserPreferenceRepository)) 4
,))4 5$
UserPreferenceRepository))6 N
>))N O
())O P
)))P Q
;))Q R
services** 
.** 
AddTransient** 
<** 
IValidationService** 0
,**0 1
ValidationService**2 C
>**C D
(**D E
)**E F
;**F G
services-- 
.-- 
	AddScoped-- 
<-- 
IMappingProvider-- +
,--+ ,
MapsterProvider--- <
>--< =
(--= >
)--> ?
;--? @
services00 
.00 
AddSingleton00 
<00 
ILoggingProvider00 .
,00. /
SerilogProvider000 ?
>00? @
(00@ A
)00A B
;00B C
services33 
.33 
	AddScoped33 
<33 
ILoggingService33 *
,33* +
LoggingService33, :
>33: ;
(33; <
)33< =
;33= >
services44 
.44 
	AddScoped44 
<44 
IValidationService44 -
,44- .
ValidationService44/ @
>44@ A
(44A B
)44B C
;44C D
services77 
.77 /
#AddValidatorsFromAssemblyContaining77 4
<774 5$
UserCreationDtoValidator775 M
>77M N
(77N O
)77O P
;77P Q
services88 
.88 /
#AddValidatorsFromAssemblyContaining88 4
<884 5
UserDtoValidator885 E
>88E F
(88F G
)88G H
;88H I
services99 
.99 -
!AddFluentValidationAutoValidation99 2
(992 3
)993 4
;994 5
services<< 
.<< 

AddSerilog<< 
(<< 
(<< 
services<< %
,<<% &
lc<<' )
)<<) *
=><<+ -
lc<<. 0
.== 
ReadFrom== 
.== 
Configuration== #
(==# $
configuration==$ 1
)==1 2
.>> 
ReadFrom>> 
.>> 
Services>> 
(>> 
services>> '
)>>' (
.?? 
Enrich?? 
.?? 
FromLogContext?? "
(??" #
)??# $
)??$ %
;??% &
returnAA 
servicesAA 
;AA 
}BB 
}CC õ
eC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.WebAPI\Extensions\MiddlewareExtensions.cs
	namespace 	
Streamphony
 
. 
WebAPI 
. 

Extensions '
;' (
public 
static 
class  
MiddlewareExtensions (
{ 
public 

static 
IApplicationBuilder %#
UseSwaggerDocumentation& =
(= >
this> B
IApplicationBuilderC V
appW Z
)Z [
{ 
app		 
.		 

UseSwagger		 
(		 
)		 
;		 
app

 
.

 
UseSwaggerUI

 
(

 
)

 
;

 
return 
app 
; 
} 
public 

static 
IApplicationBuilder %
UseRequestTiming& 6
(6 7
this7 ;
IApplicationBuilder< O
appP S
)S T
{ 
return 
app 
. 
UseMiddleware  
<  !"
RequestTimerMiddleware! 7
>7 8
(8 9
)9 :
;: ;
} 
public 

static 
IApplicationBuilder %
UseDbTransaction& 6
(6 7
this7 ;
IApplicationBuilder< O
appP S
)S T
{ 
return 
app 
. 
UseMiddleware  
<  !"
RequestTimerMiddleware! 7
>7 8
(8 9
)9 :
;: ;
} 
} —-
jC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.WebAPI\Controllers\UserPreferenceController.cs
	namespace 	
Streamphony
 
. 
WebAPI 
. 
Controllers (
;( )
[

 
Route

 
(

 
$str

 
)

 
]

 
public 
class $
UserPreferenceController %
(% &
	IMediator& /
mediator0 8
)8 9
:: ;
AppBaseController< M
{ 
private 
readonly 
	IMediator 
	_mediator (
=) *
mediator+ 3
;3 4
[ 
HttpPost 
] 
[ 
ValidateModel 
] 
public 

async 
Task 
< 
ActionResult "
<" #
UserPreferenceDto# 4
>4 5
>5 6 
CreateUserPreference7 K
(K L
UserPreferenceDtoL ]
userPreferenceDto^ o
)o p
{ 
var $
createdUserPreferenceDto $
=% &
await' ,
	_mediator- 6
.6 7
Send7 ;
(; <
new< ? 
CreateUserPreference@ T
(T U
userPreferenceDtoU f
)f g
)g h
;h i
return 
CreatedAtAction 
( 
nameof %
(% &!
GetUserPreferenceById& ;
); <
,< =
new> A
{B C
idD F
=G H$
createdUserPreferenceDtoI a
.a b
Idb d
}e f
,f g%
createdUserPreferenceDto	h Ä
)
Ä Å
;
Å Ç
} 
[ 
HttpPut 
] 
[ 
ValidateModel 
] 
public 

async 
Task 
< 
ActionResult "
<" #
UserPreferenceDto# 4
>4 5
>5 6 
UpdateUserPreference7 K
(K L
UserPreferenceDtoL ]
userPreferenceDto^ o
)o p
{ 
try 
{ 	
var $
updatedUserPreferenceDto (
=) *
await+ 0
	_mediator1 :
.: ;
Send; ?
(? @
new@ C 
UpdateUserPreferenceD X
(X Y
userPreferenceDtoY j
)j k
)k l
;l m
return 
Ok 
( $
updatedUserPreferenceDto .
). /
;/ 0
} 	
catch   
(    
KeyNotFoundException   #
)  # $
{!! 	
return"" 
NotFound"" 
("" 
$""" 
$str"" 5
{""5 6
userPreferenceDto""6 G
.""G H
Id""H J
}""J K
$str""K V
"""V W
)""W X
;""X Y
}## 	
}$$ 
[&& 
HttpGet&& 
]&& 
public'' 

async'' 
Task'' 
<'' 
ActionResult'' "
<''" #
IEnumerable''# .
<''. /
UserPreferenceDto''/ @
>''@ A
>''A B
>''B C!
GetAllUserPreferences''D Y
(''Y Z
)''Z [
{(( 
var)) 
userPreferences)) 
=)) 
await)) #
	_mediator))$ -
.))- .
Send)). 2
())2 3
new))3 6!
GetAllUserPreferences))7 L
())L M
)))M N
)))N O
;))O P
return** 
Ok** 
(** 
userPreferences** !
)**! "
;**" #
}++ 
[-- 
HttpGet-- 
(-- 
$str-- 
)-- 
]-- 
public.. 

async.. 
Task.. 
<.. 
ActionResult.. "
<.." #
UserPreferenceDto..# 4
>..4 5
>..5 6!
GetUserPreferenceById..7 L
(..L M
Guid..M Q
id..R T
)..T U
{// 
var00 
userPreferenceDto00 
=00 
await00  %
	_mediator00& /
.00/ 0
Send000 4
(004 5
new005 8!
GetUserPreferenceById009 N
(00N O
id00O Q
)00Q R
)00R S
;00S T
if11 

(11 
userPreferenceDto11 
==11  
null11! %
)11% &
return11' -
NotFound11. 6
(116 7
$str117 R
)11R S
;11S T
return22 
Ok22 
(22 
userPreferenceDto22 #
)22# $
;22$ %
}33 
[55 

HttpDelete55 
(55 
$str55 
)55 
]55 
public66 

async66 
Task66 
<66 
IActionResult66 #
>66# $ 
DeleteUserPreference66% 9
(669 :
Guid66: >
id66? A
)66A B
{77 
var88 
result88 
=88 
await88 
	_mediator88 $
.88$ %
Send88% )
(88) *
new88* - 
DeleteUserPreference88. B
(88B C
id88C E
)88E F
)88F G
;88G H
if99 

(99 
!99 
result99 
)99 
return99 
NotFound99 $
(99$ %
$"99% '
$str99' >
{99> ?
id99? A
}99A B
$str99B M
"99M N
)99N O
;99O P
return:: 
Ok:: 
(:: 
):: 
;:: 
};; 
}<< á+
`C:\Personal\amdaris\project\streamphony-asp-net\Streamphony.WebAPI\Controllers\UserController.cs
	namespace 	
Streamphony
 
. 
WebAPI 
. 
Controllers (
;( )
[

 
Route

 
(

 
$str

 
)

 
]

 
public 
class 
UserController 
( 
	IMediator %
mediator& .
). /
:0 1
AppBaseController2 C
{ 
private 
readonly 
	IMediator 
	_mediator (
=) *
mediator+ 3
;3 4
[ 
HttpPost 
] 
[ 
ValidateModel 
] 
public 

async 
Task 
< 
ActionResult "
<" #
UserDto# *
>* +
>+ ,

CreateUser- 7
(7 8
UserCreationDto8 G
userDtoH O
)O P
{ 
var 
createdUserDto 
= 
await "
	_mediator# ,
., -
Send- 1
(1 2
new2 5

CreateUser6 @
(@ A
userDtoA H
)H I
)I J
;J K
return 
CreatedAtAction 
( 
nameof %
(% &
GetUserById& 1
)1 2
,2 3
new4 7
{8 9
id: <
== >
createdUserDto? M
.M N
IdN P
}Q R
,R S
createdUserDtoT b
)b c
;c d
} 
[ 
HttpPut 
] 
[ 
ValidateModel 
] 
public 

async 
Task 
< 
ActionResult "
<" #
UserDto# *
>* +
>+ ,

UpdateUser- 7
(7 8
UserDto8 ?
userDto@ G
)G H
{ 
try 
{ 	
var 
updatedUserDto 
=  
await! &
	_mediator' 0
.0 1
Send1 5
(5 6
new6 9

UpdateUser: D
(D E
userDtoE L
)L M
)M N
;N O
return 
Ok 
( 
updatedUserDto $
)$ %
;% &
} 	
catch   
(    
KeyNotFoundException   #
)  # $
{!! 	
return"" 
NotFound"" 
("" 
$""" 
$str"" +
{""+ ,
userDto"", 3
.""3 4
Id""4 6
}""6 7
$str""7 B
"""B C
)""C D
;""D E
}## 	
}$$ 
[&& 
HttpGet&& 
]&& 
public'' 

async'' 
Task'' 
<'' 
ActionResult'' "
<''" #
IEnumerable''# .
<''. /
UserDto''/ 6
>''6 7
>''7 8
>''8 9
GetAllUsers'': E
(''E F
)''F G
{(( 
var)) 
users)) 
=)) 
await)) 
	_mediator)) #
.))# $
Send))$ (
())( )
new))) ,
GetAllUsers))- 8
())8 9
)))9 :
))): ;
;)); <
return** 
Ok** 
(** 
users** 
)** 
;** 
}++ 
[-- 
HttpGet-- 
(-- 
$str-- 
)-- 
]-- 
public.. 

async.. 
Task.. 
<.. 
ActionResult.. "
<.." #
UserDetailsDto..# 1
>..1 2
>..2 3
GetUserById..4 ?
(..? @
Guid..@ D
id..E G
)..G H
{// 
var00 
userDto00 
=00 
await00 
	_mediator00 %
.00% &
Send00& *
(00* +
new00+ .
GetUserById00/ :
(00: ;
id00; =
)00= >
)00> ?
;00? @
if11 

(11 
userDto11 
==11 
null11 
)11 
return11 #
NotFound11$ ,
(11, -
$str11- >
)11> ?
;11? @
return22 
Ok22 
(22 
userDto22 
)22 
;22 
}33 
[55 

HttpDelete55 
(55 
$str55 
)55 
]55 
public66 

async66 
Task66 
<66 
IActionResult66 #
>66# $

DeleteUser66% /
(66/ 0
Guid660 4
id665 7
)667 8
{77 
var88 
result88 
=88 
await88 
	_mediator88 $
.88$ %
Send88% )
(88) *
new88* -

DeleteUser88. 8
(888 9
id889 ;
)88; <
)88< =
;88= >
if99 

(99 
!99 
result99 
)99 
return99 
NotFound99 $
(99$ %
$"99% '
$str99' 4
{994 5
id995 7
}997 8
$str998 C
"99C D
)99D E
;99E F
return:: 
Ok:: 
(:: 
):: 
;:: 
};; 
}<< Œ+
`C:\Personal\amdaris\project\streamphony-asp-net\Streamphony.WebAPI\Controllers\SongController.cs
	namespace 	
Streamphony
 
. 
WebAPI 
. 
Controllers (
;( )
[

 
Route

 
(

 
$str

 
)

 
]

 
public 
class 
SongController 
( 
	IMediator %
mediator& .
). /
:0 1
AppBaseController2 C
{ 
private 
readonly 
	IMediator 
	_mediator (
=) *
mediator+ 3
;3 4
[ 
HttpPost 
] 
[ 
ValidateModel 
] 
public 

async 
Task 
< 
ActionResult "
<" #
SongDto# *
>* +
>+ ,

CreateSong- 7
(7 8
SongCreationDto8 G
songDtoH O
)O P
{ 
var 
createdSongDto 
= 
await "
	_mediator# ,
., -
Send- 1
(1 2
new2 5

CreateSong6 @
(@ A
songDtoA H
)H I
)I J
;J K
return 
CreatedAtAction 
( 
nameof %
(% &
GetSongById& 1
)1 2
,2 3
new4 7
{8 9
id: <
== >
createdSongDto? M
.M N
IdN P
}Q R
,R S
createdSongDtoT b
)b c
;c d
} 
[ 
HttpPut 
] 
[ 
ValidateModel 
] 
public 

async 
Task 
< 
ActionResult "
<" #
SongDto# *
>* +
>+ ,

UpdateSong- 7
(7 8
SongDto8 ?
songDto@ G
)G H
{ 
try 
{ 	
var 
updatedSongDto 
=  
await! &
	_mediator' 0
.0 1
Send1 5
(5 6
new6 9

UpdateSong: D
(D E
songDtoE L
)L M
)M N
;N O
return 
Ok 
( 
updatedSongDto $
)$ %
;% &
} 	
catch   
(    
KeyNotFoundException   #
ex  $ &
)  & '
{!! 	
return"" 
NotFound"" 
("" 
ex"" 
."" 
Message"" &
)""& '
;""' (
}## 	
}$$ 
[&& 

HttpDelete&& 
(&& 
$str&& 
)&& 
]&& 
[''  
ProducesResponseType'' 
('' 
StatusCodes'' %
.''% &
Status200OK''& 1
)''1 2
]''2 3
public(( 

async(( 
Task(( 
<(( 
IActionResult(( #
>((# $

DeleteSong((% /
(((/ 0
Guid((0 4
id((5 7
)((7 8
{)) 
var** 
result** 
=** 
await** 
	_mediator** $
.**$ %
Send**% )
(**) *
new*** -

DeleteSong**. 8
(**8 9
id**9 ;
)**; <
)**< =
;**= >
if++ 

(++ 
!++ 
result++ 
)++ 
return++ 
NotFound++ $
(++$ %
$"++% '
$str++' 4
{++4 5
id++5 7
}++7 8
$str++8 C
"++C D
)++D E
;++E F
return,, 
Ok,, 
(,, 
),, 
;,, 
}-- 
[// 
HttpGet// 
]// 
public00 

async00 
Task00 
<00 
ActionResult00 "
<00" #
IEnumerable00# .
<00. /
SongDto00/ 6
>006 7
>007 8
>008 9
GetAllSongs00: E
(00E F
)00F G
{11 
var22 
songs22 
=22 
await22 
	_mediator22 #
.22# $
Send22$ (
(22( )
new22) ,
GetAllSongs22- 8
(228 9
)229 :
)22: ;
;22; <
return33 
Ok33 
(33 
songs33 
)33 
;33 
}44 
[66 
HttpGet66 
(66 
$str66 
)66 
]66 
public77 

async77 
Task77 
<77 
ActionResult77 "
<77" #
SongDto77# *
>77* +
>77+ ,
GetSongById77- 8
(778 9
Guid779 =
id77> @
)77@ A
{88 
var99 
songDto99 
=99 
await99 
	_mediator99 %
.99% &
Send99& *
(99* +
new99+ .
GetSongById99/ :
(99: ;
id99; =
)99= >
)99> ?
;99? @
if:: 

(:: 
songDto:: 
==:: 
null:: 
):: 
return:: #
NotFound::$ ,
(::, -
$str::- >
)::> ?
;::? @
return;; 
Ok;; 
(;; 
songDto;; 
);; 
;;; 
}<< 
}== ¢+
aC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.WebAPI\Controllers\GenreController.cs
	namespace 	
Streamphony
 
. 
WebAPI 
. 
Controllers (
;( )
[

 
Route

 
(

 
$str

 
)

 
]

 
public 
class 
GenreController 
( 
	IMediator &
mediator' /
)/ 0
:1 2
AppBaseController3 D
{ 
private 
readonly 
	IMediator 
	_mediator (
=) *
mediator+ 3
;3 4
[ 
HttpPost 
] 
[ 
ValidateModel 
] 
public 

async 
Task 
< 
ActionResult "
<" #
GenreDto# +
>+ ,
>, -
CreateGenre. 9
(9 :
GenreCreationDto: J
genreDtoK S
)S T
{ 
var 
createdGenreDto 
= 
await #
	_mediator$ -
.- .
Send. 2
(2 3
new3 6
CreateGenre7 B
(B C
genreDtoC K
)K L
)L M
;M N
return 
CreatedAtAction 
( 
nameof %
(% &
GetGenreById& 2
)2 3
,3 4
new5 8
{9 :
id; =
=> ?
createdGenreDto@ O
.O P
IdP R
}S T
,T U
createdGenreDtoV e
)e f
;f g
} 
[ 
HttpPut 
] 
[ 
ValidateModel 
] 
public 

async 
Task 
< 
ActionResult "
<" #
GenreDto# +
>+ ,
>, -
UpdateGenre. 9
(9 :
GenreDto: B
genreDtoC K
)K L
{ 
try 
{ 	
var 
updatedGenreDto 
=  !
await" '
	_mediator( 1
.1 2
Send2 6
(6 7
new7 :
UpdateGenre; F
(F G
genreDtoG O
)O P
)P Q
;Q R
return 
Ok 
( 
updatedGenreDto %
)% &
;& '
} 	
catch   
(    
KeyNotFoundException   #
)  # $
{!! 	
return"" 
NotFound"" 
("" 
$""" 
$str"" ,
{"", -
genreDto""- 5
.""5 6
Id""6 8
}""8 9
$str""9 D
"""D E
)""E F
;""F G
}## 	
}$$ 
[&& 
HttpGet&& 
]&& 
public'' 

async'' 
Task'' 
<'' 
ActionResult'' "
<''" #
IEnumerable''# .
<''. /
GenreDto''/ 7
>''7 8
>''8 9
>''9 :
GetAllGenres''; G
(''G H
)''H I
{(( 
var)) 
genres)) 
=)) 
await)) 
	_mediator)) $
.))$ %
Send))% )
())) *
new))* -
GetAllGenres)). :
()): ;
))); <
)))< =
;))= >
return** 
Ok** 
(** 
genres** 
)** 
;** 
}++ 
[-- 
HttpGet-- 
(-- 
$str-- 
)-- 
]-- 
public.. 

async.. 
Task.. 
<.. 
ActionResult.. "
<.." #
GenreDto..# +
>..+ ,
>.., -
GetGenreById... :
(..: ;
Guid..; ?
id..@ B
)..B C
{// 
var00 
genreDto00 
=00 
await00 
	_mediator00 &
.00& '
Send00' +
(00+ ,
new00, /
GetGenreById000 <
(00< =
id00= ?
)00? @
)00@ A
;00A B
if11 

(11 
genreDto11 
==11 
null11 
)11 
return11 $
NotFound11% -
(11- .
$str11. @
)11@ A
;11A B
return22 
Ok22 
(22 
genreDto22 
)22 
;22 
}33 
[55 

HttpDelete55 
(55 
$str55 
)55 
]55 
public66 

async66 
Task66 
<66 
IActionResult66 #
>66# $
DeleteGenre66% 0
(660 1
Guid661 5
id666 8
)668 9
{77 
var88 
result88 
=88 
await88 
	_mediator88 $
.88$ %
Send88% )
(88) *
new88* -
DeleteGenre88. 9
(889 :
id88: <
)88< =
)88= >
;88> ?
if99 

(99 
!99 
result99 
)99 
return99 
NotFound99 $
(99$ %
$"99% '
$str99' 5
{995 6
id996 8
}998 9
$str999 D
"99D E
)99E F
;99F G
return:: 
Ok:: 
(:: 
):: 
;:: 
};; 
}<< √
cC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.WebAPI\Controllers\AppBaseController.cs
	namespace 	
Streamphony
 
. 
WebAPI 
. 
Controllers (
;( )
[ 
ApiController 
] 
public 
abstract 
class 
AppBaseController '
:( )
ControllerBase* 8
{ 
} Ä$
aC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.WebAPI\Controllers\AlbumController.cs
	namespace 	
Streamphony
 
. 
WebAPI 
. 
Controllers (
;( )
[

 
Route

 
(

 
$str

 
)

 
]

 
public 
class 
AlbumController 
( 
	IMediator &
mediator' /
)/ 0
:1 2
AppBaseController3 D
{ 
private 
readonly 
	IMediator 
	_mediator (
=) *
mediator+ 3
;3 4
[ 
HttpPost 
] 
[ 
ValidateModel 
] 
public 

async 
Task 
< 
ActionResult "
<" #
AlbumDto# +
>+ ,
>, -
CreateAlbum. 9
(9 :
AlbumCreationDto: J
albumDtoK S
)S T
{ 
var 
createdAlbumDto 
= 
await #
	_mediator$ -
.- .
Send. 2
(2 3
new3 6
CreateAlbum7 B
(B C
albumDtoC K
)K L
)L M
;M N
return 
CreatedAtAction 
( 
nameof %
(% &
GetAlbumById& 2
)2 3
,3 4
new5 8
{9 :
id; =
=> ?
createdAlbumDto@ O
.O P
IdP R
}S T
,T U
createdAlbumDtoV e
)e f
;f g
} 
[ 
HttpPut 
] 
[ 
ValidateModel 
] 
public 

async 
Task 
< 
ActionResult "
<" #
AlbumDto# +
>+ ,
>, -
UpdateAlbum. 9
(9 :
AlbumDto: B
albumDtoC K
)K L
{ 
var 
updatedAlbumDto 
= 
await #
	_mediator$ -
.- .
Send. 2
(2 3
new3 6
UpdateAlbum7 B
(B C
albumDtoC K
)K L
)L M
;M N
return 
Ok 
( 
updatedAlbumDto !
)! "
;" #
} 
[ 
HttpGet 
] 
public   

async   
Task   
<   
ActionResult   "
<  " #
IEnumerable  # .
<  . /
AlbumDto  / 7
>  7 8
>  8 9
>  9 :
GetAllAlbums  ; G
(  G H
)  H I
{!! 
var"" 
albums"" 
="" 
await"" 
	_mediator"" $
.""$ %
Send""% )
("") *
new""* -
GetAllAlbums"". :
("": ;
)""; <
)""< =
;""= >
return## 
Ok## 
(## 
albums## 
)## 
;## 
}$$ 
[&& 
HttpGet&& 
(&& 
$str&& 
)&& 
]&& 
public'' 

async'' 
Task'' 
<'' 
ActionResult'' "
<''" #
AlbumDto''# +
>''+ ,
>'', -
GetAlbumById''. :
('': ;
Guid''; ?
id''@ B
)''B C
{(( 
var)) 
albumDto)) 
=)) 
await)) 
	_mediator)) &
.))& '
Send))' +
())+ ,
new)), /
GetAlbumById))0 <
())< =
id))= ?
)))? @
)))@ A
;))A B
return** 
Ok** 
(** 
albumDto** 
)** 
;** 
}++ 
[-- 

HttpDelete-- 
(-- 
$str-- 
)-- 
]-- 
public.. 

async.. 
Task.. 
<.. 
IActionResult.. #
>..# $
DeleteAlbum..% 0
(..0 1
Guid..1 5
id..6 8
)..8 9
{// 
await00 
	_mediator00 
.00 
Send00 
(00 
new00  
DeleteAlbum00! ,
(00, -
id00- /
)00/ 0
)000 1
;001 2
return11 
	NoContent11 
(11 
)11 
;11 
}22 
}33 ä
aC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.WebAPI\Common\Models\ErrorResponse.cs
	namespace 	
Streamphony
 
. 
WebAPI 
. 
Common #
.# $
Models$ *
;* +
public 
class 
ErrorResponse 
{ 
public 

int 

StatusCode 
{ 
get 
;  
set! $
;$ %
}& '
public 

string 
StatusPhrase 
{  
get! $
;$ %
set& )
;) *
}+ ,
=- .
default/ 6
!6 7
;7 8
public		 

List		 
<		 
string		 
>		 
Errors		 
{		  
get		! $
;		$ %
}		& '
=		( )
[		* +
]		+ ,
;		, -
public

 

DateTime

 
	Timestamp

 
{

 
get

  #
;

# $
set

% (
;

( )
}

* +
public 

override 
string 
ToString #
(# $
)$ %
{ 
return 
JsonSerializer 
. 
	Serialize '
(' (
this( ,
), -
;- .
} 
} 