ö}
eC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Services\ValidationService.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
Services" *
;* +
public 
class 
ValidationService 
( 
ILoggingService .
loggingService/ =
)= >
:? @
IValidationServiceA S
{		 
private

 
readonly

 
ILoggingService

 $
_loggingService

% 4
=

5 6
loggingService

7 E
;

E F
public 

async 
Task 
< 
TEntity 
> 
GetExistingEntity 0
<0 1
TEntity1 8
>8 9
(9 :
IRepository 
< 
TEntity 
> 

repository '
,' (
Guid 
entityId 
, 
CancellationToken 
cancellationToken +
,+ ,
	LogAction 
	logAction 
= 
	LogAction '
.' (
Update( .
) 
where 
TEntity 
: 

BaseEntity  
{ 
var 

entityName 
= 
typeof 
(  
TEntity  '
)' (
.( )
Name) -
;- .
var 
existingEntity 
= 
await "

repository# -
.- .
GetById. 5
(5 6
entityId6 >
,> ?
cancellationToken@ Q
)Q R
;R S
if 

( 
existingEntity 
== 
null "
)" #
_loggingService 
. (
LogAndThrowNotFoundException 8
(8 9

entityName9 C
,C D
entityIdE M
,M N
	logActionO X
)X Y
;Y Z
return 
existingEntity 
! 
; 
} 
public 

async 
Task 
< 
TEntity 
> (
GetExistingEntityWithInclude ;
<; <
TEntity< C
>C D
(D E
Func 
< 
Guid 
, 
CancellationToken $
,$ %

Expression& 0
<0 1
Func1 5
<5 6
TEntity6 =
,= >
object? E
>E F
>F G
[G H
]H I
,I J
TaskK O
<O P
TEntityP W
?W X
>X Y
>Y Z$
getEntityWithIncludeFunc[ s
,s t
Guid 
entityId 
, 
	LogAction 
	logAction 
, 
CancellationToken   
cancellationToken   +
,  + ,
params!! 

Expression!! 
<!! 
Func!! 
<!! 
TEntity!! &
,!!& '
object!!( .
>!!. /
>!!/ 0
[!!0 1
]!!1 2
includeProperties!!3 D
)"" 
where"" 
TEntity"" 
:"" 

BaseEntity""  
{## 
var$$ 

entityName$$ 
=$$ 
typeof$$ 
($$  
TEntity$$  '
)$$' (
.$$( )
Name$$) -
;$$- .
var%% 
existingEntity%% 
=%% 
await%% "$
getEntityWithIncludeFunc%%# ;
(%%; <
entityId%%< D
,%%D E
cancellationToken%%F W
,%%W X
includeProperties%%Y j
)%%j k
;%%k l
if'' 

('' 
existingEntity'' 
=='' 
null'' "
)''" #
_loggingService(( 
.(( (
LogAndThrowNotFoundException(( 8
(((8 9

entityName((9 C
,((C D
entityId((E M
,((M N
	logAction((O X
)((X Y
;((Y Z
return** 
existingEntity** 
!** 
;** 
}++ 
public.. 

async.. 
Task.. 
AssertEntityExists.. (
<..( )
TEntity..) 0
>..0 1
(..1 2
IRepository// 
<// 
TEntity// 
>// 

repository// '
,//' (
Guid00 
entityId00 
,00 
CancellationToken11 
cancellationToken11 +
,11+ ,
	LogAction22 
	logAction22 
=22 
	LogAction22 '
.22' (
Delete22( .
)33 
where33 
TEntity33 
:33 

BaseEntity33  
{44 
var55 

entityName55 
=55 
typeof55 
(55  
TEntity55  '
)55' (
.55( )
Name55) -
;55- .
var66 
existingEntity66 
=66 
await66 "

repository66# -
.66- .
GetById66. 5
(665 6
entityId666 >
,66> ?
cancellationToken66@ Q
)66Q R
;66R S
if88 

(88 
existingEntity88 
==88 
null88 "
)88" #
_loggingService99 
.99 (
LogAndThrowNotFoundException99 8
(998 9

entityName999 C
,99C D
entityId99E M
,99M N
	logAction99O X
)99X Y
;99Y Z
}:: 
public<< 

async<< 
Task<< (
AssertNavigationEntityExists<< 2
<<<2 3
TEntity<<3 :
,<<: ;
TNav<<< @
><<@ A
(<<A B
IRepository== 
<== 
TNav== 
>== 

repository== $
,==$ %
Guid>> 
navId>> 
,>> 
CancellationToken?? 
cancellationToken?? +
,??+ ,
	LogAction@@ 
	logAction@@ 
=@@ 
	LogAction@@ '
.@@' (
Create@@( .
)AA 
whereAA 
TNavAA 
:AA 

BaseEntityAA 
{BB 
varCC 

entityNameCC 
=CC 
typeofCC 
(CC  
TEntityCC  '
)CC' (
.CC( )
NameCC) -
;CC- .
varDD 
navNameDD 
=DD 
typeofDD 
(DD 
TNavDD !
)DD! "
.DD" #
NameDD# '
;DD' (
varEE 
existingNavEE 
=EE 
awaitEE 

repositoryEE  *
.EE* +
GetByIdEE+ 2
(EE2 3
navIdEE3 8
,EE8 9
cancellationTokenEE: K
)EEK L
;EEL M
ifGG 

(GG 
existingNavGG 
==GG 
nullGG 
)GG  
_loggingServiceHH 
.HH 5
)LogAndThrowNotFoundExceptionForNavigationHH E
(HHE F

entityNameHHF P
,HHP Q
navNameHHR Y
,HHY Z
navIdHH[ `
,HH` a
	logActionHHb k
)HHk l
;HHl m
}II 
publicKK 

asyncKK 
TaskKK (
AssertNavigationEntityExistsKK 2
<KK2 3
TEntityKK3 :
,KK: ;
TNavKK< @
>KK@ A
(KKA B
IRepositoryLL 
<LL 
TNavLL 
>LL 

repositoryLL $
,LL$ %
GuidMM 
?MM 
idMM 
,MM 
CancellationTokenNN 
cancellationTokenNN +
,NN+ ,
	LogActionOO 
	logActionOO 
=OO 
	LogActionOO '
.OO' (
CreateOO( .
)PP 
wherePP 
TNavPP 
:PP 

BaseEntityPP 
{QQ 
ifRR 

(RR 
idRR 
==RR 
nullRR 
)RR 
returnRR 
;RR 
varSS 
navIdSS 
=SS 
idSS 
.SS 
ValueSS 
;SS 
awaitUU (
AssertNavigationEntityExistsUU *
<UU* +
TEntityUU+ 2
,UU2 3
TNavUU4 8
>UU8 9
(UU9 :

repositoryUU: D
,UUD E
navIdUUF K
,UUK L
cancellationTokenUUM ^
,UU^ _
	logActionUU` i
)UUi j
;UUj k
}VV 
publicXX 

asyncXX 
TaskXX  
EnsureUniquePropertyXX *
<XX* +
TEntityXX+ 2
,XX2 3
	TPropertyXX4 =
>XX= >
(XX> ?
FuncYY 
<YY 
	TPropertyYY 
,YY 
CancellationTokenYY )
,YY) *
TaskYY+ /
<YY/ 0
TEntityYY0 7
?YY7 8
>YY8 9
>YY9 :
getEntityFuncYY; H
,YYH I
stringZZ 
propertyNameZZ 
,ZZ 
	TProperty[[ 
propertyValue[[ 
,[[  
CancellationToken\\ 
cancellationToken\\ +
,\\+ ,
	LogAction]] 
	logAction]] 
=]] 
	LogAction]] '
.]]' (
Create]]( .
)]]. /
{^^ 
var__ 

entityName__ 
=__ 
typeof__ 
(__  
TEntity__  '
)__' (
.__( )
Name__) -
;__- .
var`` 
existingEntity`` 
=`` 
await`` "
getEntityFunc``# 0
(``0 1
propertyValue``1 >
,``> ?
cancellationToken``@ Q
)``Q R
;``R S
ifbb 

(bb 
existingEntitybb 
!=bb 
nullbb "
)bb" #
_loggingServicecc 
.cc )
LogAndThrowDuplicateExceptioncc 9
(cc9 :

entityNamecc: D
,ccD E
propertyNameccF R
,ccR S
propertyValueccT a
,cca b
	logActionccc l
)ccl m
;ccm n
}dd 
publicff 

asyncff 
Taskff (
EnsureUniquePropertyExceptIdff 2
<ff2 3
TEntityff3 :
,ff: ;
	TPropertyff< E
>ffE F
(ffF G
Funcgg 
<gg 
	TPropertygg 
,gg 
Guidgg 
,gg 
CancellationTokengg /
,gg/ 0
Taskgg1 5
<gg5 6
IEnumerablegg6 A
<ggA B
TEntityggB I
>ggI J
>ggJ K
>ggK L
getEntitiesFuncggM \
,gg\ ]
stringhh 
propertyNamehh 
,hh 
	TPropertyii 
propertyValueii 
,ii  
Guidjj 
entityIdjj 
,jj 
CancellationTokenkk 
cancellationTokenkk +
,kk+ ,
	LogActionll 
	logActionll 
=ll 
	LogActionll '
.ll' (
Updatell( .
)ll. /
{mm 
varnn 

entityNamenn 
=nn 
typeofnn 
(nn  
TEntitynn  '
)nn' (
.nn( )
Namenn) -
;nn- .
varoo 
existingEntitiesoo 
=oo 
awaitoo $
getEntitiesFuncoo% 4
(oo4 5
propertyValueoo5 B
,ooB C
entityIdooD L
,ooL M
cancellationTokenooN _
)oo_ `
;oo` a
ifqq 

(qq 
existingEntitiesqq 
.qq 
Anyqq  
(qq  !
)qq! "
)qq" #
_loggingServicerr 
.rr )
LogAndThrowDuplicateExceptionrr 9
(rr9 :

entityNamerr: D
,rrD E
propertyNamerrF R
,rrR S
propertyValuerrT a
,rra b
	logActionrrc l
)rrl m
;rrm n
}ss 
publicuu 

asyncuu 
Taskuu $
EnsureUserUniquePropertyuu .
<uu. /
TEntityuu/ 6
,uu6 7
	TPropertyuu8 A
>uuA B
(uuB C
Funcvv 
<vv 
Guidvv 
,vv 
	TPropertyvv 
,vv 
CancellationTokenvv /
,vv/ 0
Taskvv1 5
<vv5 6
TEntityvv6 =
?vv= >
>vv> ?
>vv? @
getEntityFuncvvA N
,vvN O
Guidww 
ownerIdww 
,ww 
stringxx 
propertyNamexx 
,xx 
	TPropertyyy 
propertyValueyy 
,yy  
CancellationTokenzz 
cancellationTokenzz +
,zz+ ,
	LogAction{{ 
	logAction{{ 
={{ 
	LogAction{{ '
.{{' (
Create{{( .
){{. /
{|| 
var}} 

entityName}} 
=}} 
typeof}} 
(}}  
TEntity}}  '
)}}' (
.}}( )
Name}}) -
;}}- .
var~~ 
existingEntity~~ 
=~~ 
await~~ "
getEntityFunc~~# 0
(~~0 1
ownerId~~1 8
,~~8 9
propertyValue~~: G
,~~G H
cancellationToken~~I Z
)~~Z [
;~~[ \
if
ÄÄ 

(
ÄÄ 
existingEntity
ÄÄ 
!=
ÄÄ 
null
ÄÄ "
)
ÄÄ" #
_loggingService
ÅÅ 
.
ÅÅ 2
$LogAndThrowDuplicateExceptionForUser
ÅÅ @
(
ÅÅ@ A

entityName
ÅÅA K
,
ÅÅK L
propertyName
ÅÅM Y
,
ÅÅY Z
propertyValue
ÅÅ[ h
,
ÅÅh i
ownerId
ÅÅj q
,
ÅÅq r
	logAction
ÅÅs |
)
ÅÅ| }
;
ÅÅ} ~
}
ÇÇ 
public
ÑÑ 

async
ÑÑ 
Task
ÑÑ .
 EnsureUserUniquePropertyExceptId
ÑÑ 6
<
ÑÑ6 7
TEntity
ÑÑ7 >
,
ÑÑ> ?
	TProperty
ÑÑ@ I
>
ÑÑI J
(
ÑÑJ K
Func
ÖÖ 
<
ÖÖ 
Guid
ÖÖ 
,
ÖÖ 
	TProperty
ÖÖ 
,
ÖÖ 
Guid
ÖÖ "
,
ÖÖ" #
CancellationToken
ÖÖ$ 5
,
ÖÖ5 6
Task
ÖÖ7 ;
<
ÖÖ; <
IEnumerable
ÖÖ< G
<
ÖÖG H
TEntity
ÖÖH O
>
ÖÖO P
>
ÖÖP Q
>
ÖÖQ R
getEntitiesFunc
ÖÖS b
,
ÖÖb c
Guid
ÜÜ 
ownerId
ÜÜ 
,
ÜÜ 
string
áá 
propertyName
áá 
,
áá 
	TProperty
àà 
propertyValue
àà 
,
àà  
Guid
ââ 
entityId
ââ 
,
ââ 
CancellationToken
ää 
cancellationToken
ää +
,
ää+ ,
	LogAction
ãã 
	logAction
ãã 
=
ãã 
	LogAction
ãã '
.
ãã' (
Update
ãã( .
)
ãã. /
{
åå 
var
çç 

entityName
çç 
=
çç 
typeof
çç 
(
çç  
TEntity
çç  '
)
çç' (
.
çç( )
Name
çç) -
;
çç- .
var
éé 
existingEntities
éé 
=
éé 
await
éé $
getEntitiesFunc
éé% 4
(
éé4 5
ownerId
éé5 <
,
éé< =
propertyValue
éé> K
,
ééK L
entityId
ééM U
,
ééU V
cancellationToken
ééW h
)
ééh i
;
ééi j
if
êê 

(
êê 
existingEntities
êê 
.
êê 
Any
êê  
(
êê  !
)
êê! "
)
êê" #
_loggingService
ëë 
.
ëë 2
$LogAndThrowDuplicateExceptionForUser
ëë @
(
ëë@ A

entityName
ëëA K
,
ëëK L
propertyName
ëëM Y
,
ëëY Z
propertyValue
ëë[ h
,
ëëh i
ownerId
ëëj q
,
ëëq r
	logAction
ëës |
)
ëë| }
;
ëë} ~
}
íí 
}ìì ¥8
bC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Services\LoggingService.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
Services" *
;* +
public 
class 
LoggingService 
( 
ILoggingProvider ,
logger- 3
)3 4
:5 6
ILoggingService7 F
{		 
private

 
readonly

 
ILoggingProvider

 %
_logger

& -
=

. /
logger

0 6
;

6 7
public 

void 

LogSuccess 
( 
string !

entityName" ,
,, -
	LogAction. 7
	logAction8 A
=B C
	LogActionD M
.M N
GetN Q
)Q R
{ 
_logger 
. 
LogInformation 
( 
$str K
,K L
	logActionM V
,V W

entityNameX b
)b c
;c d
} 
public 

void 

LogSuccess 
( 
string !

entityName" ,
,, -
Guid. 2
entityId3 ;
,; <
	LogAction= F
	logActionG P
=Q R
	LogActionS \
.\ ]
Create] c
)c d
{ 
_logger 
. 
LogInformation 
( 
$str [
,[ \
	logAction] f
,f g

entityNameh r
,r s
entityIdt |
)| }
;} ~
} 
public 

void -
!LogAndThrowNotAuthorizedException 1
(1 2
string2 8

entityName9 C
,C D
GuidE I
entityIdJ R
,R S
stringT Z
navName[ b
,b c
Guidd h
navIdi n
,n o
	LogActionp y
	logAction	z É
=
Ñ Ö
	LogAction
Ü è
.
è ê
Update
ê ñ
)
ñ ó
{ 
_logger 
. 

LogWarning 
( 
$str	 é
,
é è
	logAction
ê ô
,
ô ö

entityName
õ •
,
• ¶
entityId
ß Ø
,
Ø ∞
navName
± ∏
,
∏ π
navId
∫ ø
)
ø ¿
;
¿ ¡
throw 
new !
UnauthorizedException '
(' (
$"( *
{* +
navName+ 2
}2 3
$str3 =
{= >
navId> C
}C D
$strD S
{S T

entityNameT ^
}^ _
$str_ i
{i j
entityIdj r
}r s
$strs u
"u v
)v w
;w x
} 
public 

void (
LogAndThrowNotFoundException ,
(, -
string- 3

entityName4 >
,> ?
Guid@ D
entityIdE M
,M N
	LogActionO X
	logActionY b
)b c
{ 
_logger 
. 

LogWarning 
( 
$str d
,d e
	logActionf o
,o p

entityNameq {
,{ |
entityId	} Ö
)
Ö Ü
;
Ü á
throw 
new 
NotFoundException #
(# $
$"$ &
{& '

entityName' 1
}1 2
$str2 <
{< =
entityId= E
}E F
$strF R
"R S
)S T
;T U
}   
public"" 

void"" 5
)LogAndThrowNotFoundExceptionForNavigation"" 9
(""9 :
string"": @

entityName""A K
,""K L
string""M S
navName""T [
,""[ \
Guid""] a
navId""b g
,""g h
	LogAction""i r
	logAction""s |
)""| }
{## 
_logger$$ 
.$$ 

LogWarning$$ 
($$ 
$str$$ ~
,$$~ 
	logAction
$$Ä â
,
$$â ä

entityName
$$ã ï
,
$$ï ñ
navName
$$ó û
,
$$û ü
navId
$$† •
)
$$• ¶
;
$$¶ ß
throw%% 
new%% 
NotFoundException%% #
(%%# $
$"%%$ &
{%%& '
navName%%' .
}%%. /
$str%%/ 9
{%%9 :
navId%%: ?
}%%? @
$str%%@ L
"%%L M
)%%M N
;%%N O
}&& 
public(( 

void(( )
LogAndThrowDuplicateException(( -
<((- .
T((. /
>((/ 0
(((0 1
string((1 7

entityName((8 B
,((B C
string((D J
propertyName((K W
,((W X
T((Y Z
propertyValue(([ h
,((h i
	LogAction((j s
	logAction((t }
=((~ 
	LogAction
((Ä â
.
((â ä
Update
((ä ê
)
((ê ë
{)) 
_logger** 
.** 

LogWarning** 
(** 
$str** v
,**v w
	logAction	**x Å
,
**Å Ç

entityName
**É ç
,
**ç é
propertyName
**è õ
,
**õ ú
propertyValue
**ù ™
)
**™ ´
;
**´ ¨
throw++ 
new++ 
DuplicateException++ $
(++$ %
$"++% '
{++' (

entityName++( 2
}++2 3
$str++3 9
{++9 :
propertyName++: F
}++F G
$str++G I
{++I J
propertyValue++J W
}++W X
$str++X i
"++i j
)++j k
;++k l
},, 
public.. 

void.. 0
$LogAndThrowDuplicateExceptionForUser.. 4
<..4 5
T..5 6
>..6 7
(..7 8
string..8 >

entityName..? I
,..I J
string..K Q
propertyName..R ^
,..^ _
T..` a
propertyValue..b o
,..o p
Guid..q u
ownerId..v }
,..} ~
	LogAction	.. à
	logAction
..â í
)
..í ì
{// 
_logger00 
.00 

LogWarning00 
(00 
$str	00 §
,
00§ •
	logAction
00¶ Ø
,
00Ø ∞

entityName
00± ª
,
00ª º
propertyName
00Ω …
,
00…  
propertyValue
00À ÿ
,
00ÿ Ÿ
nameof
00⁄ ‡
(
00‡ ·
User
00· Â
)
00Â Ê
,
00Ê Á
ownerId
00Ë Ô
)
00Ô 
;
00 Ò
throw11 
new11 
DuplicateException11 $
(11$ %
$"11% '
{11' (

entityName11( 2
}112 3
$str113 9
{119 :
propertyName11: F
}11F G
$str11G I
{11I J
propertyValue11J W
}11W X
$str11X {
{11{ |
ownerId	11| É
}
11É Ñ
$str
11Ñ Ü
"
11Ü á
)
11á à
;
11à â
}22 
}33 ∏
]C:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Services\LogAction.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
Services" *
;* +
public 
enum 
	LogAction 
{ 
Create 

,
 
Update 

,
 
Delete 

,
 
Get 
}		 Œ
kC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Exceptions\UnauthorizedException.cs
	namespace 	
Streamphony
 
. 
Application !
.! "

Exceptions" ,
;, -
public 
class !
UnauthorizedException "
:# $
	Exception% .
{ 
public 
!
UnauthorizedException  
(  !
string! '
message( /
)/ 0
:1 2
base3 7
(7 8
message8 ?
)? @
{ 
} 
public		 
!
UnauthorizedException		  
(		  !
string		! '
message		( /
,		/ 0
	Exception		1 :
innerException		; I
)		I J
:		K L
base		M Q
(		Q R
message		R Y
,		Y Z
innerException		[ i
)		i j
{

 
} 
} æ
gC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Exceptions\NotFoundException.cs
	namespace 	
Streamphony
 
. 
Application !
.! "

Exceptions" ,
;, -
public 
class 
NotFoundException 
:  
	Exception! *
{ 
public 

NotFoundException 
( 
string #
message$ +
)+ ,
:- .
base/ 3
(3 4
message4 ;
); <
{ 
} 
public		 

NotFoundException		 
(		 
string		 #
message		$ +
,		+ ,
	Exception		- 6
innerException		7 E
)		E F
:		G H
base		I M
(		M N
message		N U
,		U V
innerException		W e
)		e f
{

 
} 
} ¬
hC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Exceptions\DuplicateException.cs
	namespace 	
Streamphony
 
. 
Application !
.! "

Exceptions" ,
;, -
public 
class 
DuplicateException 
:  !
	Exception" +
{ 
public 

DuplicateException 
( 
string $
message% ,
), -
:. /
base0 4
(4 5
message5 <
)< =
{ 
} 
public		 

DuplicateException		 
(		 
string		 $
message		% ,
,		, -
	Exception		. 7
innerException		8 F
)		F G
:		H I
base		J N
(		N O
message		O V
,		V W
innerException		X f
)		f g
{

 
} 
} …
fC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Users\Responses\UserDto.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Users& +
.+ ,
	Responses, 5
;5 6
public 
class 
UserDto 
: 
UserCreationDto &
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
} å
mC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Users\Responses\UserDetailsDto.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Users& +
.+ ,
	Responses, 5
;5 6
public 
class 
UserDetailsDto 
: 
UserDto %
{ 
public		 

ICollection		 
<		 
SongDto		 
?		 
>		  
UploadedSongs		! .
{		/ 0
get		1 4
;		4 5
set		6 9
;		9 :
}		; <
=		= >
new		? B
HashSet		C J
<		J K
SongDto		K R
?		R S
>		S T
(		T U
)		U V
;		V W
public

 

ICollection

 
<

 
AlbumDto

 
?

  
>

  !
OwnedAlbums

" -
{

. /
get

0 3
;

3 4
set

5 8
;

8 9
}

: ;
=

< =
new

> A
HashSet

B I
<

I J
AlbumDto

J R
?

R S
>

S T
(

T U
)

U V
;

V W
public 

UserPreferenceDto 
? 
Preferences )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
} É

nC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Users\Responses\UserCreationDto.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Users& +
.+ ,
	Responses, 5
;5 6
public 
class 
UserCreationDto 
{ 
public 

required 
string 
Username #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 

required 
string 
Email  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 

required 
string 

ArtistName %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 

DateOnly 
DateOfBirth 
{  !
get" %
;% &
set' *
;* +
}, -
public		 

string		 
?		 
ProfilePictureUrl		 $
{		% &
get		' *
;		* +
set		, /
;		/ 0
}		1 2
}

 ≥
hC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Users\Queries\GetUserById.cs
	namespace		 	
Streamphony		
 
.		 
Application		 !
.		! "
App		" %
.		% &
Users		& +
.		+ ,
Queries		, 3
;		3 4
public 
record 
GetUserById 
( 
Guid 
Id !
)! "
:# $
IRequest% -
<- .
UserDetailsDto. <
>< =
;= >
public 
class 
GetUserByIdHandler 
(  
IUnitOfWork  +

unitOfWork, 6
,6 7
IMappingProvider8 H
mapperI O
,O P
ILoggingServiceQ `
loggera g
,g h
IValidationServicei {
validationService	| ç
)
ç é
:
è ê
IRequestHandler
ë †
<
† °
GetUserById
° ¨
,
¨ ≠
UserDetailsDto
Æ º
>
º Ω
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
IMappingProvider %
_mapper& -
=. /
mapper0 6
;6 7
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
private 
readonly 
IValidationService '
_validationService( :
=; <
validationService= N
;N O
public 

async 
Task 
< 
UserDetailsDto $
>$ %
Handle& ,
(, -
GetUserById- 8
request9 @
,@ A
CancellationTokenB S
cancellationTokenT e
)e f
{ 
var 
user 
= 
await 
_validationService +
.+ ,(
GetExistingEntityWithInclude, H
<H I
UserI M
>M N
(N O
_unitOfWork 
. 
UserRepository &
.& '
GetByIdWithInclude' 9
,9 :
request 
. 
Id 
, 
	LogAction 
. 
Get 
, 
cancellationToken 
, 
user 
=> 
user 
. 
UploadedSongs &
,& '
user 
=> 
user 
. 
Preferences $
,$ %
user 
=> 
user 
. 
OwnedAlbums $
) 	
;	 

_logger   
.   

LogSuccess   
(   
nameof   !
(  ! "
User  " &
)  & '
,  ' (
user  ) -
.  - .
Id  . 0
,  0 1
	LogAction  2 ;
.  ; <
Get  < ?
)  ? @
;  @ A
return!! 
_mapper!! 
.!! 
Map!! 
<!! 
UserDetailsDto!! )
>!!) *
(!!* +
user!!+ /
)!!/ 0
;!!0 1
}"" 
}## ‚
hC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Users\Queries\GetAllUsers.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Users& +
.+ ,
Queries, 3
;3 4
public

 
class

 
GetAllUsers

 
(

 
)

 
:

 
IRequest

 %
<

% &
IEnumerable

& 1
<

1 2
UserDto

2 9
>

9 :
>

: ;
;

; <
public 
class 
GetAllUsersHandler 
(  
IUnitOfWork  +

unitOfWork, 6
,6 7
IMappingProvider8 H
mapperI O
,O P
ILoggingServiceQ `
loggera g
)g h
:i j
IRequestHandlerk z
<z {
GetAllUsers	{ Ü
,
Ü á
IEnumerable
à ì
<
ì î
UserDto
î õ
>
õ ú
>
ú ù
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
IMappingProvider %
_mapper& -
=. /
mapper0 6
;6 7
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
public 

async 
Task 
< 
IEnumerable !
<! "
UserDto" )
>) *
>* +
Handle, 2
(2 3
GetAllUsers3 >
request? F
,F G
CancellationTokenH Y
cancellationTokenZ k
)k l
{ 
var 
users 
= 
await 
_unitOfWork %
.% &
UserRepository& 4
.4 5
GetAll5 ;
(; <
cancellationToken< M
)M N
;N O
_logger 
. 

LogSuccess 
( 
nameof !
(! "
User" &
)& '
)' (
;( )
return 
_mapper 
. 
Map 
< 
IEnumerable &
<& '
UserDto' .
>. /
>/ 0
(0 1
users1 6
)6 7
;7 8
} 
} ¿-
hC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Users\Commands\UpdateUser.cs
	namespace		 	
Streamphony		
 
.		 
Application		 !
.		! "
App		" %
.		% &
Users		& +
.		+ ,
Commands		, 4
;		4 5
public 
record 

UpdateUser 
( 
UserDto  
UserDto! (
)( )
:* +
IRequest, 4
<4 5
UserDto5 <
>< =
;= >
public 
class 
UpdateUserHandler 
:  
IRequestHandler! 0
<0 1

UpdateUser1 ;
,; <
UserDto= D
>D E
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
;, -
private 
readonly 
IMappingProvider %
_mapper& -
;- .
private 
readonly 
ILoggingService $
_logger% ,
;, -
private 
readonly 
IValidationService '
_validationService( :
;: ;
public 

UpdateUserHandler 
( 
IUnitOfWork (

unitOfWork) 3
,3 4
IMappingProvider5 E
mapperF L
,L M
ILoggingServiceN ]
logger^ d
,d e
IValidationServicef x
validationService	y ä
)
ä ã
{ 
_unitOfWork 
= 

unitOfWork  
;  !
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
_validationService 
= 
validationService .
;. /
} 
public 

async 
Task 
< 
UserDto 
> 
Handle %
(% &

UpdateUser& 0
request1 8
,8 9
CancellationToken: K
cancellationTokenL ]
)] ^
{ 
var 
userDto 
= 
request 
. 
UserDto %
;% &
var 
user 
= 
await 
_validationService +
.+ ,
GetExistingEntity, =
(= >
_unitOfWork> I
.I J
UserRepositoryJ X
,X Y
userDtoZ a
.a b
Idb d
,d e
cancellationTokenf w
)w x
;x y
await   0
$EnsureUniqueUsernameAndEmailExceptId   2
(  2 3
userDto  3 :
.  : ;
Username  ; C
,  C D
userDto  E L
.  L M
Email  M R
,  R S
userDto  T [
.  [ \
Id  \ ^
,  ^ _
cancellationToken  ` q
)  q r
;  r s
_mapper"" 
."" 
Map"" 
("" 
userDto"" 
,"" 
user"" !
)""! "
;""" #
await## 
_unitOfWork## 
.## 
	SaveAsync## #
(### $
cancellationToken##$ 5
)##5 6
;##6 7
_logger%% 
.%% 

LogSuccess%% 
(%% 
nameof%% !
(%%! "
User%%" &
)%%& '
,%%' (
user%%) -
.%%- .
Id%%. 0
,%%0 1
	LogAction%%2 ;
.%%; <
Update%%< B
)%%B C
;%%C D
return&& 
_mapper&& 
.&& 
Map&& 
<&& 
UserDto&& "
>&&" #
(&&# $
user&&$ (
)&&( )
;&&) *
}'' 
private)) 
async)) 
Task)) 0
$EnsureUniqueUsernameAndEmailExceptId)) ;
()); <
string))< B
username))C K
,))K L
string))M S
email))T Y
,))Y Z
Guid))[ _
id))` b
,))b c
CancellationToken))d u
cancellationToken	))v á
)
))á à
{** 
var++ 
conflictingUser++ 
=++ 
await++ #
_unitOfWork++$ /
.++/ 0
UserRepository++0 >
.++> ?/
#GetByUsernameOrEmailWhereIdNotEqual++? b
(++b c
username++c k
,++k l
email++m r
,++r s
id++t v
,++v w
cancellationToken	++x â
)
++â ä
;
++ä ã
if,, 

(,, 
conflictingUser,, 
!=,, 
null,, #
),,# $
{-- 	
if.. 
(.. 
conflictingUser.. 
...  
Username..  (
==..) +
username.., 4
)..4 5
{// 
_logger00 
.00 )
LogAndThrowDuplicateException00 5
(005 6
nameof006 <
(00< =
User00= A
)00A B
,00B C
$str00D N
,00N O
username00P X
,00X Y
	LogAction00Z c
.00c d
Update00d j
)00j k
;00k l
}11 
if22 
(22 
conflictingUser22 
.22  
Email22  %
==22& (
email22) .
)22. /
{33 
_logger44 
.44 )
LogAndThrowDuplicateException44 5
(445 6
nameof446 <
(44< =
User44= A
)44A B
,44B C
$str44D K
,44K L
email44M R
,44R S
	LogAction44T ]
.44] ^
Update44^ d
)44d e
;44e f
}55 
}66 	
}77 
}88 •
hC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Users\Commands\DeleteUser.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Users& +
.+ ,
Commands, 4
;4 5
public		 
record		 

DeleteUser		 
(		 
Guid		 
Id		  
)		  !
:		" #
IRequest		$ ,
<		, -
bool		- 1
>		1 2
;		2 3
public 
class 
DeleteUserHandler 
( 
IUnitOfWork *

unitOfWork+ 5
,5 6
ILoggingService7 F
loggerG M
,M N
IValidationServiceO a
validationServiceb s
)s t
:u v
IRequestHandler	w Ü
<
Ü á

DeleteUser
á ë
,
ë í
bool
ì ó
>
ó ò
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
private 
readonly 
IValidationService '
_validationService( :
=; <
validationService= N
;N O
public 

async 
Task 
< 
bool 
> 
Handle "
(" #

DeleteUser# -
request. 5
,5 6
CancellationToken7 H
cancellationTokenI Z
)Z [
{ 
Guid 
userId 
= 
request 
. 
Id  
;  !
await 
_validationService  
.  !
AssertEntityExists! 3
(3 4
_unitOfWork4 ?
.? @
UserRepository@ N
,N O
userIdP V
,V W
cancellationTokenX i
)i j
;j k
await 
_unitOfWork 
. 
SongRepository (
.( )
DeleteWhere) 4
(4 5
song5 9
=>: <
song= A
.A B
OwnerIdB I
==J L
userIdM S
,S T
cancellationTokenU f
)f g
;g h
await 
_unitOfWork 
. 
UserRepository (
.( )
Delete) /
(/ 0
userId0 6
,6 7
cancellationToken8 I
)I J
;J K
await 
_unitOfWork 
. 
	SaveAsync #
(# $
cancellationToken$ 5
)5 6
;6 7
_logger 
. 

LogSuccess 
( 
nameof !
(! "
User" &
)& '
,' (
userId) /
,/ 0
	LogAction1 :
.: ;
Delete; A
)A B
;B C
return 
true 
; 
} 
} °)
hC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Users\Commands\CreateUser.cs
	namespace		 	
Streamphony		
 
.		 
Application		 !
.		! "
App		" %
.		% &
Users		& +
.		+ ,
Commands		, 4
;		4 5
public 
record 

CreateUser 
( 
UserCreationDto (
UserCreationDto) 8
)8 9
:: ;
IRequest< D
<D E
UserDtoE L
>L M
;M N
public 
class 
CreateUserHandler 
:  
IRequestHandler! 0
<0 1

CreateUser1 ;
,; <
UserDto= D
>D E
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
;, -
private 
readonly 
IMappingProvider %
_mapper& -
;- .
private 
readonly 
ILoggingService $
_logger% ,
;, -
public 

CreateUserHandler 
( 
IUnitOfWork (

unitOfWork) 3
,3 4
IMappingProvider5 E
mapperF L
,L M
ILoggingServiceN ]
logger^ d
)d e
{ 
_unitOfWork 
= 

unitOfWork  
;  !
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
UserDto 
> 
Handle %
(% &

CreateUser& 0
request1 8
,8 9
CancellationToken: K
cancellationTokenL ]
)] ^
{ 
var 
userDto 
= 
request 
. 
UserCreationDto -
;- .
await (
EnsureUniqueUsernameAndEmail *
(* +
userDto+ 2
.2 3
Username3 ;
,; <
userDto= D
.D E
EmailE J
,J K
cancellationTokenL ]
)] ^
;^ _
var 

userEntity 
= 
_mapper  
.  !
Map! $
<$ %
User% )
>) *
(* +
userDto+ 2
)2 3
;3 4
var   
userDb   
=   
await   
_unitOfWork   &
.  & '
UserRepository  ' 5
.  5 6
Add  6 9
(  9 :

userEntity  : D
,  D E
cancellationToken  F W
)  W X
;  X Y
await!! 
_unitOfWork!! 
.!! 
	SaveAsync!! #
(!!# $
cancellationToken!!$ 5
)!!5 6
;!!6 7
_logger## 
.## 

LogSuccess## 
(## 
nameof## !
(##! "
User##" &
)##& '
,##' (
userDb##) /
.##/ 0
Id##0 2
)##2 3
;##3 4
return$$ 
_mapper$$ 
.$$ 
Map$$ 
<$$ 
UserDto$$ "
>$$" #
($$# $
userDb$$$ *
)$$* +
;$$+ ,
}%% 
private'' 
async'' 
Task'' (
EnsureUniqueUsernameAndEmail'' 3
(''3 4
string''4 :
username''; C
,''C D
string''E K
email''L Q
,''Q R
CancellationToken''S d
cancellationToken''e v
)''v w
{(( 
var)) 
conflictingUser)) 
=)) 
await)) #
_unitOfWork))$ /
.))/ 0
UserRepository))0 >
.))> ? 
GetByUsernameOrEmail))? S
())S T
username))T \
,))\ ]
email))^ c
,))c d
cancellationToken))e v
)))v w
;))w x
if** 

(** 
conflictingUser** 
!=** 
null** #
)**# $
{++ 	
if,, 
(,, 
conflictingUser,, 
.,,  
Username,,  (
==,,) +
username,,, 4
),,4 5
{-- 
_logger.. 
... )
LogAndThrowDuplicateException.. 5
(..5 6
nameof..6 <
(..< =
User..= A
)..A B
,..B C
$str..D N
,..N O
username..P X
,..X Y
	LogAction..Z c
...c d
Create..d j
)..j k
;..k l
}// 
if00 
(00 
conflictingUser00 
.00  
Email00  %
==00& (
email00) .
)00. /
{11 
_logger22 
.22 )
LogAndThrowDuplicateException22 5
(225 6
nameof226 <
(22< =
User22= A
)22A B
,22B C
$str22D K
,22K L
email22M R
,22R S
	LogAction22T ]
.22] ^
Create22^ d
)22d e
;22e f
}33 
}44 	
}55 
}66 ä
zC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\UserPreferences\Responses\UserPreferenceDto.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
UserPreferences& 5
.5 6
	Responses6 ?
;? @
public 
class 
UserPreferenceDto 
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
public 

bool 
DarkMode 
{ 
get 
; 
set  #
;# $
}% &
public 

string 
? 
Language 
{ 
get !
;! "
set# &
;& '
}( )
} ô
|C:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\UserPreferences\Queries\GetUserPreferenceById.cs
	namespace		 	
Streamphony		
 
.		 
Application		 !
.		! "
App		" %
.		% &
UserPreferences		& 5
.		5 6
Queries		6 =
;		= >
public 
record !
GetUserPreferenceById #
(# $
Guid$ (
Id) +
)+ ,
:- .
IRequest/ 7
<7 8
UserPreferenceDto8 I
>I J
;J K
public 
class (
GetUserPreferenceByIdHandler )
() *
IUnitOfWork* 5

unitOfWork6 @
,@ A
IMappingProviderB R
mapperS Y
,Y Z
ILoggingService[ j
loggerk q
,q r
IValidationService	s Ö
validationService
Ü ó
)
ó ò
:
ô ö
IRequestHandler
õ ™
<
™ ´#
GetUserPreferenceById
´ ¿
,
¿ ¡
UserPreferenceDto
¬ ”
>
” ‘
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
IMappingProvider %
_mapper& -
=. /
mapper0 6
;6 7
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
private 
readonly 
IValidationService '
_validationService( :
=; <
validationService= N
;N O
public 

async 
Task 
< 
UserPreferenceDto '
>' (
Handle) /
(/ 0!
GetUserPreferenceById0 E
requestF M
,M N
CancellationTokenO `
cancellationTokena r
)r s
{ 
var 
userPreference 
= 
await "
_validationService# 5
.5 6
GetExistingEntity6 G
(G H
_unitOfWorkH S
.S T$
UserPreferenceRepositoryT l
,l m
requestn u
.u v
Idv x
,x y
cancellationToken	z ã
,
ã å
	LogAction
ç ñ
.
ñ ó
Get
ó ö
)
ö õ
;
õ ú
_logger 
. 

LogSuccess 
( 
nameof !
(! "
UserPreference" 0
)0 1
,1 2
userPreference3 A
.A B
IdB D
,D E
	LogActionF O
.O P
GetP S
)S T
;T U
return 
_mapper 
. 
Map 
< 
UserPreferenceDto ,
>, -
(- .
userPreference. <
)< =
;= >
} 
} ˝
|C:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\UserPreferences\Queries\GetAllUserPreferences.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
UserPreferences& 5
.5 6
Queries6 =
;= >
public

 
class

 !
GetAllUserPreferences

 "
(

" #
)

# $
:

% &
IRequest

' /
<

/ 0
IEnumerable

0 ;
<

; <
UserPreferenceDto

< M
>

M N
>

N O
;

O P
public 
class (
GetAllUserPreferencesHandler )
() *
IUnitOfWork* 5

unitOfWork6 @
,@ A
IMappingProviderB R
mapperS Y
,Y Z
ILoggingService[ j
loggerk q
)q r
:s t
IRequestHandler	u Ñ
<
Ñ Ö#
GetAllUserPreferences
Ö ö
,
ö õ
IEnumerable
ú ß
<
ß ®
UserPreferenceDto
® π
>
π ∫
>
∫ ª
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
IMappingProvider %
_mapper& -
=. /
mapper0 6
;6 7
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
public 

async 
Task 
< 
IEnumerable !
<! "
UserPreferenceDto" 3
>3 4
>4 5
Handle6 <
(< =!
GetAllUserPreferences= R
requestS Z
,Z [
CancellationToken\ m
cancellationTokenn 
)	 Ä
{ 
var 
userPreferences 
= 
await #
_unitOfWork$ /
./ 0$
UserPreferenceRepository0 H
.H I
GetAllI O
(O P
cancellationTokenP a
)a b
;b c
_logger 
. 

LogSuccess 
( 
nameof !
(! "
UserPreference" 0
)0 1
)1 2
;2 3
return 
_mapper 
. 
Map 
< 
IEnumerable &
<& '
UserPreferenceDto' 8
>8 9
>9 :
(: ;
userPreferences; J
)J K
;K L
} 
} “
|C:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\UserPreferences\Commands\UpdateUserPreference.cs
	namespace		 	
Streamphony		
 
.		 
Application		 !
.		! "
App		" %
.		% &
UserPreferences		& 5
.		5 6
Commands		6 >
;		> ?
public 
record  
UpdateUserPreference "
(" #
UserPreferenceDto# 4
UserPreferenceDto5 F
)F G
:H I
IRequestJ R
<R S
UserPreferenceDtoS d
>d e
;e f
public 
class '
UpdateUserPreferenceHandler (
(( )
IUnitOfWork) 4

unitOfWork5 ?
,? @
IMappingProviderA Q
mapperR X
,X Y
ILoggingServiceZ i
loggerj p
,p q
IValidationService	r Ñ
validationService
Ö ñ
)
ñ ó
:
ò ô
IRequestHandler
ö ©
<
© ™"
UpdateUserPreference
™ æ
,
æ ø
UserPreferenceDto
¿ —
>
— “
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
IMappingProvider %
_mapper& -
=. /
mapper0 6
;6 7
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
private 
readonly 
IValidationService '
_validationService( :
=; <
validationService= N
;N O
public 

async 
Task 
< 
UserPreferenceDto '
>' (
Handle) /
(/ 0 
UpdateUserPreference0 D
requestE L
,L M
CancellationTokenN _
cancellationToken` q
)q r
{ 
var 
userPreferenceDto 
= 
request  '
.' (
UserPreferenceDto( 9
;9 :
var 
userPreference 
= 
await "
_validationService# 5
.5 6
GetExistingEntity6 G
(G H
_unitOfWorkH S
.S T$
UserPreferenceRepositoryT l
,l m
userPreferenceDton 
.	 Ä
Id
Ä Ç
,
Ç É
cancellationToken
Ñ ï
)
ï ñ
;
ñ ó
_mapper 
. 
Map 
( 
userPreferenceDto %
,% &
userPreference' 5
)5 6
;6 7
await 
_unitOfWork 
. 
	SaveAsync #
(# $
cancellationToken$ 5
)5 6
;6 7
_logger 
. 

LogSuccess 
( 
nameof !
(! "
UserPreference" 0
)0 1
,1 2
userPreference3 A
.A B
IdB D
,D E
	LogActionF O
.O P
UpdateP V
)V W
;W X
return 
_mapper 
. 
Map 
< 
UserPreferenceDto ,
>, -
(- .
userPreference. <
)< =
;= >
} 
} Ú
|C:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\UserPreferences\Commands\DeleteUserPreference.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
UserPreferences& 5
.5 6
Commands6 >
;> ?
public		 
record		  
DeleteUserPreference		 "
(		" #
Guid		# '
Id		( *
)		* +
:		, -
IRequest		. 6
<		6 7
bool		7 ;
>		; <
;		< =
public 
class '
DeleteUserPreferenceHandler (
(( )
IUnitOfWork) 4

unitOfWork5 ?
,? @
ILoggingServiceA P
loggerQ W
,W X
IValidationServiceY k
validationServicel }
)} ~
:	 Ä
IRequestHandler
Å ê
<
ê ë"
DeleteUserPreference
ë •
,
• ¶
bool
ß ´
>
´ ¨
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
private 
readonly 
IValidationService '
_validationService( :
=; <
validationService= N
;N O
public 

async 
Task 
< 
bool 
> 
Handle "
(" # 
DeleteUserPreference# 7
request8 ?
,? @
CancellationTokenA R
cancellationTokenS d
)d e
{ 
var 
userPreferenceId 
= 
request &
.& '
Id' )
;) *
await 
_validationService  
.  !
AssertEntityExists! 3
(3 4
_unitOfWork4 ?
.? @$
UserPreferenceRepository@ X
,X Y
userPreferenceIdZ j
,j k
cancellationTokenl }
)} ~
;~ 
await 
_unitOfWork 
. $
UserPreferenceRepository 2
.2 3
Delete3 9
(9 :
request: A
.A B
IdB D
,D E
cancellationTokenF W
)W X
;X Y
await 
_unitOfWork 
. 
	SaveAsync #
(# $
cancellationToken$ 5
)5 6
;6 7
_logger 
. 

LogSuccess 
( 
nameof !
(! "
UserPreference" 0
)0 1
,1 2
userPreferenceId3 C
,C D
	LogActionE N
.N O
DeleteO U
)U V
;V W
return 
true 
; 
} 
} ã%
|C:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\UserPreferences\Commands\CreateUserPreference.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
UserPreferences& 5
.5 6
Commands6 >
;> ?
public

 
record

  
CreateUserPreference

 "
(

" #
UserPreferenceDto

# 4
UserPreferenceDto

5 F
)

F G
:

H I
IRequest

J R
<

R S
UserPreferenceDto

S d
>

d e
;

e f
public 
class '
CreateUserPreferenceHandler (
:) *
IRequestHandler+ :
<: ; 
CreateUserPreference; O
,O P
UserPreferenceDtoQ b
>b c
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
;, -
private 
readonly 
IMappingProvider %
_mapper& -
;- .
private 
readonly 
ILoggingService $
_logger% ,
;, -
private 
readonly 
IValidationService '
_validationService( :
;: ;
public 
'
CreateUserPreferenceHandler &
(& '
IUnitOfWork' 2

unitOfWork3 =
,= >
IMappingProvider? O
mapperP V
,V W
ILoggingServiceX g
loggerh n
,n o
IValidationService	p Ç
validationService
É î
)
î ï
{ 
_unitOfWork 
= 

unitOfWork  
;  !
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
_validationService 
= 
validationService .
;. /
} 
public 

async 
Task 
< 
UserPreferenceDto '
>' (
Handle) /
(/ 0 
CreateUserPreference0 D
requestE L
,L M
CancellationTokenN _
cancellationToken` q
)q r
{ 
var 
userPreferenceDto 
= 
request  '
.' (
UserPreferenceDto( 9
;9 :
var 
getByIdFunc 
= 
_unitOfWork %
.% &$
UserPreferenceRepository& >
.> ?
GetById? F
;F G
await   
_validationService    
.    !(
AssertNavigationEntityExists  ! =
<  = >
UserPreference  > L
,  L M
User  N R
>  R S
(  S T
_unitOfWork  T _
.  _ `
UserRepository  ` n
,  n o
userPreferenceDto	  p Å
.
  Å Ç
Id
  Ç Ñ
,
  Ñ Ö
cancellationToken
  Ü ó
)
  ó ò
;
  ò ô
await!! 
_validationService!!  
.!!  ! 
EnsureUniqueProperty!!! 5
(!!5 6
getByIdFunc!!6 A
,!!A B
nameof!!C I
(!!I J
userPreferenceDto!!J [
.!![ \
Id!!\ ^
)!!^ _
,!!_ `
userPreferenceDto!!a r
.!!r s
Id!!s u
,!!u v
cancellationToken	!!w à
)
!!à â
;
!!â ä
var##  
userPreferenceEntity##  
=##! "
_mapper### *
.##* +
Map##+ .
<##. /
UserPreference##/ =
>##= >
(##> ?
userPreferenceDto##? P
)##P Q
;##Q R
var$$ 
userPreferenceDb$$ 
=$$ 
await$$ $
_unitOfWork$$% 0
.$$0 1$
UserPreferenceRepository$$1 I
.$$I J
Add$$J M
($$M N 
userPreferenceEntity$$N b
,$$b c
cancellationToken$$d u
)$$u v
;$$v w
await%% 
_unitOfWork%% 
.%% 
	SaveAsync%% #
(%%# $
cancellationToken%%$ 5
)%%5 6
;%%6 7
_logger'' 
.'' 

LogSuccess'' 
('' 
nameof'' !
(''! "
UserPreference''" 0
)''0 1
,''1 2
userPreferenceDb''3 C
.''C D
Id''D F
)''F G
;''G H
return(( 
_mapper(( 
.(( 
Map(( 
<(( 
UserPreferenceDto(( ,
>((, -
(((- .
userPreferenceDb((. >
)((> ?
;((? @
})) 
}** …
fC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Songs\Responses\SongDto.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Songs& +
.+ ,
	Responses, 5
;5 6
public 
class 
SongDto 
: 
SongCreationDto &
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
} ¸

nC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Songs\Responses\SongCreationDto.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Songs& +
.+ ,
	Responses, 5
;5 6
public 
class 
SongCreationDto 
{ 
public 

required 
string 
Title  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 

TimeSpan 
Duration 
{ 
get "
;" #
set$ '
;' (
}) *
public 

required 
string 
Url 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 

Guid 
OwnerId 
{ 
get 
; 
set "
;" #
}$ %
public		 

Guid		 
?		 
GenreId		 
{		 
get		 
;		 
set		  #
;		# $
}		% &
public

 

Guid

 
?

 
AlbumId

 
{

 
get

 
;

 
set

  #
;

# $
}

% &
} Û
hC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Songs\Queries\GetSongById.cs
	namespace		 	
Streamphony		
 
.		 
Application		 !
.		! "
App		" %
.		% &
Songs		& +
.		+ ,
Queries		, 3
;		3 4
public 
record 
GetSongById 
( 
Guid 
Id !
)! "
:# $
IRequest% -
<- .
SongDto. 5
>5 6
;6 7
public 
class 
GetSongByIdHandler 
(  
IUnitOfWork  +

unitOfWork, 6
,6 7
IMappingProvider8 H
mapperI O
,O P
ILoggingServiceQ `
loggera g
,g h
IValidationServicei {
validationService	| ç
)
ç é
:
è ê
IRequestHandler
ë †
<
† °
GetSongById
° ¨
,
¨ ≠
SongDto
Æ µ
>
µ ∂
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
IMappingProvider %
_mapper& -
=. /
mapper0 6
;6 7
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
private 
readonly 
IValidationService '
_validationService( :
=; <
validationService= N
;N O
public 

async 
Task 
< 
SongDto 
> 
Handle %
(% &
GetSongById& 1
request2 9
,9 :
CancellationToken; L
cancellationTokenM ^
)^ _
{ 
var 
song 
= 
await 
_validationService +
.+ ,
GetExistingEntity, =
(= >
_unitOfWork> I
.I J
SongRepositoryJ X
,X Y
requestZ a
.a b
Idb d
,d e
cancellationTokenf w
,w x
	LogAction	y Ç
.
Ç É
Get
É Ü
)
Ü á
;
á à
_logger 
. 

LogSuccess 
( 
nameof !
(! "
Song" &
)& '
,' (
song) -
.- .
Id. 0
,0 1
	LogAction2 ;
.; <
Get< ?
)? @
;@ A
return 
_mapper 
. 
Map 
< 
SongDto "
>" #
(# $
song$ (
)( )
;) *
} 
} ‚
hC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Songs\Queries\GetAllSongs.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Songs& +
.+ ,
Queries, 3
;3 4
public

 
class

 
GetAllSongs

 
(

 
)

 
:

 
IRequest

 %
<

% &
IEnumerable

& 1
<

1 2
SongDto

2 9
>

9 :
>

: ;
;

; <
public 
class 
GetAllSongsHandler 
(  
IUnitOfWork  +

unitOfWork, 6
,6 7
IMappingProvider8 H
mapperI O
,O P
ILoggingServiceQ `
loggera g
)g h
:i j
IRequestHandlerk z
<z {
GetAllSongs	{ Ü
,
Ü á
IEnumerable
à ì
<
ì î
SongDto
î õ
>
õ ú
>
ú ù
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
IMappingProvider %
_mapper& -
=. /
mapper0 6
;6 7
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
public 

async 
Task 
< 
IEnumerable !
<! "
SongDto" )
>) *
>* +
Handle, 2
(2 3
GetAllSongs3 >
request? F
,F G
CancellationTokenH Y
cancellationTokenZ k
)k l
{ 
var 
songs 
= 
await 
_unitOfWork %
.% &
SongRepository& 4
.4 5
GetAll5 ;
(; <
cancellationToken< M
)M N
;N O
_logger 
. 

LogSuccess 
( 
nameof !
(! "
Song" &
)& '
)' (
;( )
return 
_mapper 
. 
Map 
< 
IEnumerable &
<& '
SongDto' .
>. /
>/ 0
(0 1
songs1 6
)6 7
;7 8
} 
} Ú6
hC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Songs\Commands\UpdateSong.cs
	namespace		 	
Streamphony		
 
.		 
Application		 !
.		! "
App		" %
.		% &
Songs		& +
.		+ ,
Commands		, 4
;		4 5
public 
record 

UpdateSong 
( 
SongDto  
SongDto! (
)( )
:* +
IRequest, 4
<4 5
SongDto5 <
>< =
;= >
public 
class 
UpdateSongHandler 
:  
IRequestHandler! 0
<0 1

UpdateSong1 ;
,; <
SongDto= D
>D E
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
;, -
private 
readonly 
IMappingProvider %
_mapper& -
;- .
private 
readonly 
ILoggingService $
_logger% ,
;, -
private 
readonly 
IValidationService '
_validationService( :
;: ;
public 

UpdateSongHandler 
( 
IUnitOfWork (

unitOfWork) 3
,3 4
IMappingProvider5 E
mapperF L
,L M
ILoggingServiceN ]
logger^ d
,d e
IValidationServicef x
validationService	y ä
)
ä ã
{ 
_unitOfWork 
= 

unitOfWork  
;  !
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
_validationService 
= 
validationService .
;. /
} 
public 

async 
Task 
< 
SongDto 
> 
Handle %
(% &

UpdateSong& 0
request1 8
,8 9
CancellationToken: K
cancellationTokenL ]
)] ^
{ 
var 
songDto 
= 
request 
. 
SongDto %
;% &
var '
duplicateTitleForOtherSongs '
=( )
_unitOfWork* 5
.5 6
SongRepository6 D
.D E/
#GetByOwnerIdAndTitleWhereIdNotEqualE h
;h i
var!! 
song!! 
=!! 
await!! 
_validationService!! +
.!!+ ,
GetExistingEntity!!, =
(!!= >
_unitOfWork!!> I
.!!I J
SongRepository!!J X
,!!X Y
songDto!!Z a
.!!a b
Id!!b d
,!!d e
cancellationToken!!f w
)!!w x
;!!x y
await"" 
ValidateOwnership"" 
(""  
songDto""  '
,""' (
cancellationToken"") :
)"": ;
;""; <
await## 
_validationService##  
.##  !(
AssertNavigationEntityExists##! =
<##= >
Song##> B
,##B C
Genre##D I
>##I J
(##J K
_unitOfWork##K V
.##V W
GenreRepository##W f
,##f g
songDto##h o
.##o p
GenreId##p w
,##w x
cancellationToken	##y ä
,
##ä ã
	LogAction
##å ï
.
##ï ñ
Update
##ñ ú
)
##ú ù
;
##ù û
await$$ 
_validationService$$  
.$$  !(
AssertNavigationEntityExists$$! =
<$$= >
Song$$> B
,$$B C
Album$$D I
>$$I J
($$J K
_unitOfWork$$K V
.$$V W
AlbumRepository$$W f
,$$f g
songDto$$h o
.$$o p
AlbumId$$p w
,$$w x
cancellationToken	$$y ä
,
$$ä ã
	LogAction
$$å ï
.
$$ï ñ
Update
$$ñ ú
)
$$ú ù
;
$$ù û
await%% 
_validationService%%  
.%%  !,
 EnsureUserUniquePropertyExceptId%%! A
(%%A B'
duplicateTitleForOtherSongs%%B ]
,%%] ^
songDto%%_ f
.%%f g
OwnerId%%g n
,%%n o
nameof%%p v
(%%v w
songDto%%w ~
.%%~ 
Title	%% Ñ
)
%%Ñ Ö
,
%%Ö Ü
songDto
%%á é
.
%%é è
Title
%%è î
,
%%î ï
songDto
%%ñ ù
.
%%ù û
Id
%%û †
,
%%† °
cancellationToken
%%¢ ≥
)
%%≥ ¥
;
%%¥ µ
_mapper'' 
.'' 
Map'' 
('' 
songDto'' 
,'' 
song'' !
)''! "
;''" #
await(( 
_unitOfWork(( 
.(( 
	SaveAsync(( #
(((# $
cancellationToken(($ 5
)((5 6
;((6 7
_logger** 
.** 

LogSuccess** 
(** 
nameof** !
(**! "
Song**" &
)**& '
,**' (
song**) -
.**- .
Id**. 0
,**0 1
	LogAction**2 ;
.**; <
Update**< B
)**B C
;**C D
return++ 
_mapper++ 
.++ 
Map++ 
<++ 
SongDto++ "
>++" #
(++# $
song++$ (
)++( )
;++) *
},, 
private.. 
async.. 
Task.. 
ValidateOwnership.. (
(..( )
SongDto..) 0
songDto..1 8
,..8 9
CancellationToken..: K
cancellationToken..L ]
)..] ^
{// 
var00 
user00 
=00 
await00 
_validationService00 +
.00+ ,
GetExistingEntity00, =
(00= >
_unitOfWork00> I
.00I J
UserRepository00J X
,00X Y
songDto00Z a
.00a b
OwnerId00b i
,00i j
cancellationToken00k |
,00| }
	LogAction	00~ á
.
00á à
Get
00à ã
)
00ã å
;
00å ç
if22 

(22 
!22 
user22 
.22 
UploadedSongs22 
.22  
Any22  #
(22# $
song22$ (
=>22) +
song22, 0
.220 1
Id221 3
==224 6
songDto227 >
.22> ?
Id22? A
)22A B
)22B C
{33 	
_logger44 
.44 -
!LogAndThrowNotAuthorizedException44 5
(445 6
nameof446 <
(44< =
Song44= A
)44A B
,44B C
songDto44D K
.44K L
Id44L N
,44N O
nameof44P V
(44V W
User44W [
)44[ \
,44\ ]
songDto44^ e
.44e f
OwnerId44f m
)44m n
;44n o
}55 	
}66 
}77 Œ
hC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Songs\Commands\DeleteSong.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Songs& +
.+ ,
Commands, 4
;4 5
public		 
record		 

DeleteSong		 
(		 
Guid		 
Id		  
)		  !
:		" #
IRequest		$ ,
<		, -
bool		- 1
>		1 2
;		2 3
public 
class 
DeleteSongHandler 
( 
IUnitOfWork *

unitOfWork+ 5
,5 6
ILoggingService7 F
loggerG M
,M N
IValidationServiceO a
validationServiceb s
)s t
:u v
IRequestHandler	w Ü
<
Ü á

DeleteSong
á ë
,
ë í
bool
ì ó
>
ó ò
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
private 
readonly 
IValidationService '
_validationService( :
=; <
validationService= N
;N O
public 

async 
Task 
< 
bool 
> 
Handle "
(" #

DeleteSong# -
request. 5
,5 6
CancellationToken7 H
cancellationTokenI Z
)Z [
{ 
var 
songId 
= 
request 
. 
Id 
;  
await 
_validationService  
.  !
AssertEntityExists! 3
(3 4
_unitOfWork4 ?
.? @
SongRepository@ N
,N O
songIdP V
,V W
cancellationTokenX i
)i j
;j k
await 
_unitOfWork 
. 
SongRepository (
.( )
Delete) /
(/ 0
songId0 6
,6 7
cancellationToken8 I
)I J
;J K
await 
_unitOfWork 
. 
	SaveAsync #
(# $
cancellationToken$ 5
)5 6
;6 7
_logger 
. 

LogSuccess 
( 
nameof !
(! "
Song" &
)& '
,' (
songId) /
,/ 0
	LogAction1 :
.: ;
Delete; A
)A B
;B C
return 
true 
; 
} 
} …*
hC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Songs\Commands\CreateSong.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Songs& +
.+ ,
Commands, 4
;4 5
public

 
record

 

CreateSong

 
(

 
SongCreationDto

 (
SongCreationDto

) 8
)

8 9
:

: ;
IRequest

< D
<

D E
SongDto

E L
>

L M
;

M N
public 
class 
CreateSongHandler 
:  
IRequestHandler! 0
<0 1

CreateSong1 ;
,; <
SongDto= D
>D E
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
;, -
private 
readonly 
IMappingProvider %
_mapper& -
;- .
private 
readonly 
ILoggingService $
_logger% ,
;, -
private 
readonly 
IValidationService '
_validationService( :
;: ;
public 

CreateSongHandler 
( 
IUnitOfWork (

unitOfWork) 3
,3 4
IMappingProvider5 E
mapperF L
,L M
ILoggingServiceN ]
logger^ d
,d e
IValidationServicef x
validationService	y ä
)
ä ã
{ 
_unitOfWork 
= 

unitOfWork  
;  !
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
_validationService 
= 
validationService .
;. /
} 
public 

async 
Task 
< 
SongDto 
> 
Handle %
(% &

CreateSong& 0
request1 8
,8 9
CancellationToken: K
cancellationTokenL ]
)] ^
{ 
var 
songDto 
= 
request 
. 
SongCreationDto -
;- .
var $
getByOwnerIdAndTitleFunc $
=% &
_unitOfWork' 2
.2 3
SongRepository3 A
.A B 
GetByOwnerIdAndTitleB V
;V W
await   
_validationService    
.    !(
AssertNavigationEntityExists  ! =
<  = >
Song  > B
,  B C
User  D H
>  H I
(  I J
_unitOfWork  J U
.  U V
UserRepository  V d
,  d e
songDto  f m
.  m n
OwnerId  n u
,  u v
cancellationToken	  w à
)
  à â
;
  â ä
await!! 
_validationService!!  
.!!  !(
AssertNavigationEntityExists!!! =
<!!= >
Song!!> B
,!!B C
Genre!!D I
>!!I J
(!!J K
_unitOfWork!!K V
.!!V W
GenreRepository!!W f
,!!f g
songDto!!h o
.!!o p
GenreId!!p w
,!!w x
cancellationToken	!!y ä
)
!!ä ã
;
!!ã å
await"" 
_validationService""  
.""  !(
AssertNavigationEntityExists""! =
<""= >
Song""> B
,""B C
Album""D I
>""I J
(""J K
_unitOfWork""K V
.""V W
AlbumRepository""W f
,""f g
songDto""h o
.""o p
AlbumId""p w
,""w x
cancellationToken	""y ä
)
""ä ã
;
""ã å
await## 
_validationService##  
.##  !$
EnsureUserUniqueProperty##! 9
(##9 :$
getByOwnerIdAndTitleFunc##: R
,##R S
songDto##T [
.##[ \
OwnerId##\ c
,##c d
nameof##e k
(##k l
songDto##l s
.##s t
Title##t y
)##y z
,##z {
songDto	##| É
.
##É Ñ
Title
##Ñ â
,
##â ä
cancellationToken
##ã ú
)
##ú ù
;
##ù û
var%% 

songEntity%% 
=%% 
_mapper%%  
.%%  !
Map%%! $
<%%$ %
Song%%% )
>%%) *
(%%* +
songDto%%+ 2
)%%2 3
;%%3 4
var&& 
songDb&& 
=&& 
await&& 
_unitOfWork&& &
.&&& '
SongRepository&&' 5
.&&5 6
Add&&6 9
(&&9 :

songEntity&&: D
,&&D E
cancellationToken&&F W
)&&W X
;&&X Y
await'' 
_unitOfWork'' 
.'' 
	SaveAsync'' #
(''# $
cancellationToken''$ 5
)''5 6
;''6 7
_logger)) 
.)) 

LogSuccess)) 
()) 
nameof)) !
())! "
Song))" &
)))& '
,))' (
songDb))) /
.))/ 0
Id))0 2
)))2 3
;))3 4
return** 
_mapper** 
.** 
Map** 
<** 
SongDto** "
>**" #
(**# $
songDb**$ *
)*** +
;**+ ,
}++ 
},, Œ
hC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Genres\Responses\GenreDto.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Genres& ,
., -
	Responses- 6
;6 7
public 
class 
GenreDto 
: 
GenreCreationDto (
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
} ƒ
oC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Genres\Responses\GenreDetailsDto.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Genres& ,
., -
	Responses- 6
;6 7
public 
class 
GenreDetailsDto 
: 
GenreDto '
{ 
public 

ICollection 
< 
SongDto 
? 
>  
Songs! &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
=5 6
new7 :
HashSet; B
<B C
SongDtoC J
?J K
>K L
(L M
)M N
;N O
} Ä
pC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Genres\Responses\GenreCreationDto.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Genres& ,
., -
	Responses- 6
;6 7
public 
class 
GenreCreationDto 
{ 
public 

required 
string 
Name 
{  !
get" %
;% &
set' *
;* +
}, -
public 

required 
string 
Description &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
} Ë
jC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Genres\Queries\GetGenreById.cs
	namespace		 	
Streamphony		
 
.		 
Application		 !
.		! "
App		" %
.		% &
Genres		& ,
.		, -
Queries		- 4
;		4 5
public 
record 
GetGenreById 
( 
Guid 
Id  "
)" #
:$ %
IRequest& .
<. /
GenreDetailsDto/ >
>> ?
;? @
public 
class 
GetGenreByIdHandler  
(  !
IUnitOfWork! ,

unitOfWork- 7
,7 8
IMappingProvider9 I
mapperJ P
,P Q
ILoggingServiceR a
loggerb h
,h i
IValidationServicej |
validationService	} é
)
é è
:
ê ë
IRequestHandler
í °
<
° ¢
GetGenreById
¢ Æ
,
Æ Ø
GenreDetailsDto
∞ ø
>
ø ¿
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
IMappingProvider %
_mapper& -
=. /
mapper0 6
;6 7
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
private 
readonly 
IValidationService '
_validationService( :
=; <
validationService= N
;N O
public 

async 
Task 
< 
GenreDetailsDto %
>% &
Handle' -
(- .
GetGenreById. :
request; B
,B C
CancellationTokenD U
cancellationTokenV g
)g h
{ 
var 
genre 
= 
await 
_validationService ,
., -(
GetExistingEntityWithInclude- I
<I J
GenreJ O
>O P
(P Q
_unitOfWork 
. 
GenreRepository '
.' (
GetByIdWithInclude( :
,: ;
request 
. 
Id 
, 
	LogAction 
. 
Get 
, 
cancellationToken 
, 
genre 
=> 
genre 
. 
Songs  
) 	
;	 

_logger 
. 

LogSuccess 
( 
nameof !
(! "
Genre" '
)' (
,( )
genre* /
./ 0
Id0 2
,2 3
	LogAction4 =
.= >
Get> A
)A B
;B C
return 
_mapper 
. 
Map 
< 
GenreDetailsDto *
>* +
(+ ,
genre, 1
)1 2
;2 3
}   
}!! Ò
jC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Genres\Queries\GetAllGenres.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Genres& ,
., -
Queries- 4
;4 5
public

 
class

 
GetAllGenres

 
(

 
)

 
:

 
IRequest

 &
<

& '
IEnumerable

' 2
<

2 3
GenreDto

3 ;
>

; <
>

< =
;

= >
public 
class 
GetAllGenresHandler  
(  !
IUnitOfWork! ,

unitOfWork- 7
,7 8
IMappingProvider9 I
mapperJ P
,P Q
ILoggingServiceR a
loggerb h
)h i
:j k
IRequestHandlerl {
<{ |
GetAllGenres	| à
,
à â
IEnumerable
ä ï
<
ï ñ
GenreDto
ñ û
>
û ü
>
ü †
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
IMappingProvider %
_mapper& -
=. /
mapper0 6
;6 7
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
public 

async 
Task 
< 
IEnumerable !
<! "
GenreDto" *
>* +
>+ ,
Handle- 3
(3 4
GetAllGenres4 @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ 
var 
genres 
= 
await 
_unitOfWork &
.& '
GenreRepository' 6
.6 7
GetAll7 =
(= >
cancellationToken> O
)O P
;P Q
_logger 
. 

LogSuccess 
( 
nameof !
(! "
Genre" '
)' (
)( )
;) *
return 
_mapper 
. 
Map 
< 
IEnumerable &
<& '
GenreDto' /
>/ 0
>0 1
(1 2
genres2 8
)8 9
;9 :
} 
} …
jC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Genres\Commands\UpdateGenre.cs
	namespace		 	
Streamphony		
 
.		 
Application		 !
.		! "
App		" %
.		% &
Genres		& ,
.		, -
Commands		- 5
;		5 6
public 
record 
UpdateGenre 
( 
GenreDto "
GenreDto# +
)+ ,
:- .
IRequest/ 7
<7 8
GenreDto8 @
>@ A
;A B
public 
class 
UpdateGenreHandler 
(  
IUnitOfWork  +

unitOfWork, 6
,6 7
IMappingProvider8 H
mapperI O
,O P
ILoggingServiceQ `
loggera g
,g h
IValidationServicei {
validationService	| ç
)
ç é
:
è ê
IRequestHandler
ë †
<
† °
UpdateGenre
° ¨
,
¨ ≠
GenreDto
Æ ∂
>
∂ ∑
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
IMappingProvider %
_mapper& -
=. /
mapper0 6
;6 7
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
private 
readonly 
IValidationService '
_validationService( :
=; <
validationService= N
;N O
public 

async 
Task 
< 
GenreDto 
> 
Handle  &
(& '
UpdateGenre' 2
request3 :
,: ;
CancellationToken< M
cancellationTokenN _
)_ `
{ 
var 
genreDto 
= 
request 
. 
GenreDto '
;' (
var '
duplicateNameForOtherGenres '
=( )
_unitOfWork* 5
.5 6
GenreRepository6 E
.E F$
GetByNameWhereIdNotEqualF ^
;^ _
var 
genre 
= 
await 
_validationService ,
., -
GetExistingEntity- >
(> ?
_unitOfWork? J
.J K
GenreRepositoryK Z
,Z [
genreDto\ d
.d e
Ide g
,g h
cancellationTokeni z
)z {
;{ |
await 
_validationService  
.  !(
EnsureUniquePropertyExceptId! =
(= >'
duplicateNameForOtherGenres> Y
,Y Z
nameof[ a
(a b
genreDtob j
.j k
Namek o
)o p
,p q
genreDtor z
.z {
Name{ 
,	 Ä
genreDto
Å â
.
â ä
Id
ä å
,
å ç
cancellationToken
é ü
)
ü †
;
† °
_mapper 
. 
Map 
( 
genreDto 
, 
genre #
)# $
;$ %
await 
_unitOfWork 
. 
	SaveAsync #
(# $
cancellationToken$ 5
)5 6
;6 7
_logger 
. 

LogSuccess 
( 
nameof !
(! "
Genre" '
)' (
,( )
genre* /
./ 0
Id0 2
,2 3
	LogAction4 =
.= >
Update> D
)D E
;E F
return   
_mapper   
.   
Map   
<   
GenreDto   #
>  # $
(  $ %
genre  % *
)  * +
;  + ,
}!! 
}"" ˚
jC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Genres\Commands\DeleteGenre.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Genres& ,
., -
Commands- 5
;5 6
public		 
record		 
DeleteGenre		 
(		 
Guid		 
Id		 !
)		! "
:		# $
IRequest		% -
<		- .
bool		. 2
>		2 3
;		3 4
public 
class 
DeleteGenreHandler 
(  
IUnitOfWork  +

unitOfWork, 6
,6 7
ILoggingService8 G
loggerH N
,N O
IValidationServiceP b
validationServicec t
)t u
:v w
IRequestHandler	x á
<
á à
DeleteGenre
à ì
,
ì î
bool
ï ô
>
ô ö
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
private 
readonly 
IValidationService '
_validationService( :
=; <
validationService= N
;N O
public 

async 
Task 
< 
bool 
> 
Handle "
(" #
DeleteGenre# .
request/ 6
,6 7
CancellationToken8 I
cancellationTokenJ [
)[ \
{ 
var 
genreId 
= 
request 
. 
Id  
;  !
await 
_validationService  
.  !
AssertEntityExists! 3
(3 4
_unitOfWork4 ?
.? @
GenreRepository@ O
,O P
genreIdQ X
,X Y
cancellationTokenZ k
)k l
;l m
await 
_unitOfWork 
. 
GenreRepository )
.) *
Delete* 0
(0 1
request1 8
.8 9
Id9 ;
,; <
cancellationToken= N
)N O
;O P
await 
_unitOfWork 
. 
	SaveAsync #
(# $
cancellationToken$ 5
)5 6
;6 7
_logger 
. 

LogSuccess 
( 
nameof !
(! "
Genre" '
)' (
,( )
genreId* 1
,1 2
	LogAction3 <
.< =
Delete= C
)C D
;D E
return 
true 
; 
} 
} ≥
jC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Genres\Commands\CreateGenre.cs
	namespace		 	
Streamphony		
 
.		 
Application		 !
.		! "
App		" %
.		% &
Genres		& ,
.		, -
Commands		- 5
;		5 6
public 
record 
CreateGenre 
( 
GenreCreationDto *
GenreCreationDto+ ;
); <
:= >
IRequest? G
<G H
GenreDtoH P
>P Q
;Q R
public 
class 
CreateGenreHandler 
(  
IUnitOfWork  +

unitOfWork, 6
,6 7
IMappingProvider8 H
mapperI O
,O P
ILoggingServiceQ `
loggera g
,g h
IValidationServicei {
validationService	| ç
)
ç é
:
è ê
IRequestHandler
ë †
<
† °
CreateGenre
° ¨
,
¨ ≠
GenreDto
Æ ∂
>
∂ ∑
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
IMappingProvider %
_mapper& -
=. /
mapper0 6
;6 7
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
private 
readonly 
IValidationService '
_validationService( :
=; <
validationService= N
;N O
public 

async 
Task 
< 
GenreDto 
> 
Handle  &
(& '
CreateGenre' 2
request3 :
,: ;
CancellationToken< M
cancellationTokenN _
)_ `
{ 
var 
genreDto 
= 
request 
. 
GenreCreationDto /
;/ 0
var 
getByNameFunc 
= 
_unitOfWork '
.' (
GenreRepository( 7
.7 8
	GetByName8 A
;A B
await 
_validationService  
.  ! 
EnsureUniqueProperty! 5
(5 6
getByNameFunc6 C
,C D
nameofE K
(K L
genreDtoL T
.T U
NameU Y
)Y Z
,Z [
genreDto\ d
.d e
Namee i
,i j
cancellationTokenk |
)| }
;} ~
var 
genreEntity 
= 
_mapper !
.! "
Map" %
<% &
Genre& +
>+ ,
(, -
request- 4
.4 5
GenreCreationDto5 E
)E F
;F G
var 
genreDb 
= 
await 
_unitOfWork '
.' (
GenreRepository( 7
.7 8
Add8 ;
(; <
genreEntity< G
,G H
cancellationTokenI Z
)Z [
;[ \
await 
_unitOfWork 
. 
	SaveAsync #
(# $
cancellationToken$ 5
)5 6
;6 7
_logger 
. 

LogSuccess 
( 
nameof !
(! "
Genre" '
)' (
,( )
genreDb* 1
.1 2
Id2 4
)4 5
;5 6
return   
_mapper   
.   
Map   
<   
GenreDto   #
>  # $
(  $ %
genreDb  % ,
)  , -
;  - .
}!! 
}"" Õ
gC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Album\Responses\AlbumDto.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Albums& ,
., -
	Responses- 6
;6 7
public 
class 
AlbumDto 
: 
AlbumCreationDto (
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
} ø
nC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Album\Responses\AlbumDetailsDto.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Albums& ,
., -
	Responses- 6
;6 7
public 
class 
AlbumDetailsDto 
: 
AlbumDto '
{ 
public 

HashSet 
< 
SongDto 
? 
> 
Songs "
{# $
get% (
;( )
set* -
;- .
}/ 0
=1 2
new3 6
HashSet7 >
<> ?
SongDto? F
?F G
>G H
(H I
)I J
;J K
} µ
oC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Album\Responses\AlbumCreationDto.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Albums& ,
., -
	Responses- 6
;6 7
public 
class 
AlbumCreationDto 
{ 
public 

required 
string 
Title  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 

string 
? 
CoverImageUrl  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 

DateOnly 
ReleaseDate 
{  !
get" %
;% &
set' *
;* +
}, -
public 

Guid 
OwnerId 
{ 
get 
; 
set "
;" #
}$ %
}		 
iC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Album\Queries\GetAllAlbums.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Albums& ,
., -
Queries- 4
;4 5
public

 
class

 
GetAllAlbums

 
(

 
)

 
:

 
IRequest

 &
<

& '
IEnumerable

' 2
<

2 3
AlbumDto

3 ;
>

; <
>

< =
;

= >
public 
class 
GetAllAlbumsHandler  
(  !
IUnitOfWork! ,

unitOfWork- 7
,7 8
IMappingProvider9 I
mapperJ P
,P Q
ILoggingServiceR a
loggerb h
)h i
:j k
IRequestHandlerl {
<{ |
GetAllAlbums	| à
,
à â
IEnumerable
ä ï
<
ï ñ
AlbumDto
ñ û
>
û ü
>
ü †
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
IMappingProvider %
_mapper& -
=. /
mapper0 6
;6 7
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
public 

async 
Task 
< 
IEnumerable !
<! "
AlbumDto" *
>* +
>+ ,
Handle- 3
(3 4
GetAllAlbums4 @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ 
var 
albums 
= 
await 
_unitOfWork &
.& '
AlbumRepository' 6
.6 7
GetAll7 =
(= >
cancellationToken> O
)O P
;P Q
_logger 
. 

LogSuccess 
( 
nameof !
(! "
Album" '
)' (
)( )
;) *
return 
_mapper 
. 
Map 
< 
IEnumerable &
<& '
AlbumDto' /
>/ 0
>0 1
(1 2
albums2 8
)8 9
;9 :
} 
} Á
iC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Album\Queries\GetAlbumById.cs
	namespace		 	
Streamphony		
 
.		 
Application		 !
.		! "
App		" %
.		% &
Albums		& ,
.		, -
Queries		- 4
;		4 5
public 
record 
GetAlbumById 
( 
Guid 
Id  "
)" #
:$ %
IRequest& .
<. /
AlbumDetailsDto/ >
>> ?
;? @
public 
class 
GetAlbumByIdHandler  
(  !
IUnitOfWork! ,

unitOfWork- 7
,7 8
IMappingProvider9 I
mapperJ P
,P Q
ILoggingServiceR a
loggerb h
,h i
IValidationServicej |
validationService	} é
)
é è
:
ê ë
IRequestHandler
í °
<
° ¢
GetAlbumById
¢ Æ
,
Æ Ø
AlbumDetailsDto
∞ ø
>
ø ¿
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
IMappingProvider %
_mapper& -
=. /
mapper0 6
;6 7
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
private 
readonly 
IValidationService '
_validationService( :
=; <
validationService= N
;N O
public 

async 
Task 
< 
AlbumDetailsDto %
>% &
Handle' -
(- .
GetAlbumById. :
request; B
,B C
CancellationTokenD U
cancellationTokenV g
)g h
{ 
var 
album 
= 
await 
_validationService ,
., -(
GetExistingEntityWithInclude- I
<I J
AlbumJ O
>O P
(P Q
_unitOfWork 
. 
AlbumRepository '
.' (
GetByIdWithInclude( :
,: ;
request 
. 
Id 
, 
	LogAction 
. 
Get 
, 
cancellationToken 
, 
album 
=> 
album 
. 
Songs  
) 	
;	 

_logger 
. 

LogSuccess 
( 
nameof !
(! "
Album" '
)' (
,( )
album* /
./ 0
Id0 2
,2 3
	LogAction4 =
.= >
Get> A
)A B
;B C
return 
_mapper 
. 
Map 
< 
AlbumDetailsDto *
>* +
(+ ,
album, 1
)1 2
;2 3
}   
}!! ù/
iC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Album\Commands\UpdateAlbum.cs
	namespace

 	
Streamphony


 
.

 
Application

 !
.

! "
App

" %
.

% &
Albums

& ,
.

, -
Commands

- 5
;

5 6
public 
record 
UpdateAlbum 
( 
AlbumDto "
AlbumDto# +
)+ ,
:- .
IRequest/ 7
<7 8
AlbumDto8 @
>@ A
;A B
public 
class 
UpdateAlbumHandler 
:  !
IRequestHandler" 1
<1 2
UpdateAlbum2 =
,= >
AlbumDto? G
>G H
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
;, -
private 
readonly 
IMappingProvider %
_mapper& -
;- .
private 
readonly 
ILoggingService $
_logger% ,
;, -
private 
readonly 
IValidationService '
_validationService( :
;: ;
public 

UpdateAlbumHandler 
( 
IUnitOfWork )

unitOfWork* 4
,4 5
IMappingProvider6 F
mapperG M
,M N
ILoggingServiceO ^
logger_ e
,e f
IValidationServiceg y
validationService	z ã
)
ã å
{ 
_unitOfWork 
= 

unitOfWork  
;  !
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
_validationService 
= 
validationService .
;. /
} 
public 

async 
Task 
< 
AlbumDto 
> 
Handle  &
(& '
UpdateAlbum' 2
request3 :
,: ;
CancellationToken< M
cancellationTokenN _
)_ `
{ 
var 
albumDto 
= 
request 
. 
AlbumDto '
;' (
var   (
duplicateTitleForOtherAlbums   (
=  ) *
_unitOfWork  + 6
.  6 7
AlbumRepository  7 F
.  F G/
#GetByOwnerIdAndTitleWhereIdNotEqual  G j
;  j k
var"" 
album"" 
="" 
await"" 
_validationService"" ,
."", -
GetExistingEntity""- >
(""> ?
_unitOfWork""? J
.""J K
AlbumRepository""K Z
,""Z [
albumDto""\ d
.""d e
Id""e g
,""g h
cancellationToken""i z
)""z {
;""{ |
await## 
ValidateOwnership## 
(##  
albumDto##  (
,##( )
cancellationToken##* ;
)##; <
;##< =
await$$ 
_validationService$$  
.$$  !,
 EnsureUserUniquePropertyExceptId$$! A
($$A B(
duplicateTitleForOtherAlbums$$B ^
,$$^ _
album$$` e
.$$e f
OwnerId$$f m
,$$m n
nameof$$o u
($$u v
albumDto$$v ~
.$$~ 
Title	$$ Ñ
)
$$Ñ Ö
,
$$Ö Ü
albumDto
$$á è
.
$$è ê
Title
$$ê ï
,
$$ï ñ
albumDto
$$ó ü
.
$$ü †
Id
$$† ¢
,
$$¢ £
cancellationToken
$$§ µ
)
$$µ ∂
;
$$∂ ∑
_mapper&& 
.&& 
Map&& 
(&& 
albumDto&& 
,&& 
album&& #
)&&# $
;&&$ %
await'' 
_unitOfWork'' 
.'' 
	SaveAsync'' #
(''# $
cancellationToken''$ 5
)''5 6
;''6 7
_logger)) 
.)) 

LogSuccess)) 
()) 
nameof)) !
())! "
Album))" '
)))' (
,))( )
album))* /
.))/ 0
Id))0 2
,))2 3
	LogAction))4 =
.))= >
Update))> D
)))D E
;))E F
return** 
_mapper** 
.** 
Map** 
<** 
AlbumDto** #
>**# $
(**$ %
album**% *
)*** +
;**+ ,
}++ 
private-- 
async-- 
Task-- 
ValidateOwnership-- (
(--( )
AlbumDto--) 1
albumDto--2 :
,--: ;
CancellationToken--< M
cancellationToken--N _
)--_ `
{.. 
var// 
user// 
=// 
await// 
_validationService// +
.//+ ,
GetExistingEntity//, =
(//= >
_unitOfWork//> I
.//I J
UserRepository//J X
,//X Y
albumDto//Z b
.//b c
OwnerId//c j
,//j k
cancellationToken//l }
,//} ~
	LogAction	// à
.
//à â
Get
//â å
)
//å ç
;
//ç é
if11 

(11 
!11 
user11 
.11 
OwnedAlbums11 
.11 
Any11 !
(11! "
album11" '
=>11( *
album11+ 0
.110 1
Id111 3
==114 6
albumDto117 ?
.11? @
Id11@ B
)11B C
)11C D
{22 	
_logger33 
.33 -
!LogAndThrowNotAuthorizedException33 5
(335 6
nameof336 <
(33< =
Album33= B
)33B C
,33C D
albumDto33E M
.33M N
Id33N P
,33P Q
nameof33R X
(33X Y
User33Y ]
)33] ^
,33^ _
albumDto33` h
.33h i
OwnerId33i p
)33p q
;33q r
}44 	
}55 
}66 Å
iC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Album\Commands\DeleteAlbum.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
App" %
.% &
Albums& ,
., -
Commands- 5
;5 6
public		 
record		 
DeleteAlbum		 
(		 
Guid		 
Id		 !
)		! "
:		# $
IRequest		% -
<		- .
Unit		. 2
>		2 3
;		3 4
public 
class 
DeleteAlbumHandler 
(  
IUnitOfWork  +

unitOfWork, 6
,6 7
ILoggingService8 G
loggerH N
,N O
IValidationServiceP b
validationServicec t
)t u
:v w
IRequestHandler	x á
<
á à
DeleteAlbum
à ì
,
ì î
Unit
ï ô
>
ô ö
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
=- .

unitOfWork/ 9
;9 :
private 
readonly 
ILoggingService $
_logger% ,
=- .
logger/ 5
;5 6
private 
readonly 
IValidationService '
_validationService( :
=; <
validationService= N
;N O
public 

async 
Task 
< 
Unit 
> 
Handle "
(" #
DeleteAlbum# .
request/ 6
,6 7
CancellationToken8 I
cancellationTokenJ [
)[ \
{ 
var 
albumdId 
= 
request 
. 
Id !
;! "
await 
_validationService  
.  !
AssertEntityExists! 3
(3 4
_unitOfWork4 ?
.? @
AlbumRepository@ O
,O P
albumdIdQ Y
,Y Z
cancellationToken[ l
)l m
;m n
await 
_unitOfWork 
. 
AlbumRepository )
.) *
Delete* 0
(0 1
albumdId1 9
,9 :
cancellationToken; L
)L M
;M N
await 
_unitOfWork 
. 
	SaveAsync #
(# $
cancellationToken$ 5
)5 6
;6 7
_logger 
. 

LogSuccess 
( 
nameof !
(! "
Album" '
)' (
,( )
albumdId* 2
,2 3
	LogAction4 =
.= >
Delete> D
)D E
;E F
return 
Unit 
. 
Value 
; 
} 
} ë$
iC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\App\Album\Commands\CreateAlbum.cs
	namespace		 	
Streamphony		
 
.		 
Application		 !
.		! "
App		" %
.		% &
Albums		& ,
.		, -
Commands		- 5
;		5 6
public 
record 
CreateAlbum 
( 
AlbumCreationDto *
AlbumCreationDto+ ;
); <
:= >
IRequest? G
<G H
AlbumDtoH P
>P Q
;Q R
public 
class 
CreateAlbumHandler 
:  !
IRequestHandler" 1
<1 2
CreateAlbum2 =
,= >
AlbumDto? G
>G H
{ 
private 
readonly 
IUnitOfWork  
_unitOfWork! ,
;, -
private 
readonly 
IMappingProvider %
_mapper& -
;- .
private 
readonly 
ILoggingService $
_logger% ,
;, -
private 
readonly 
IValidationService '
_validationService( :
;: ;
public 

CreateAlbumHandler 
( 
IUnitOfWork )

unitOfWork* 4
,4 5
IMappingProvider6 F
mapperG M
,M N
ILoggingServiceO ^
logger_ e
,e f
IValidationServiceg y
validationService	z ã
)
ã å
{ 
_unitOfWork 
= 

unitOfWork  
;  !
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
_validationService 
= 
validationService .
;. /
} 
public 

async 
Task 
< 
AlbumDto 
> 
Handle  &
(& '
CreateAlbum' 2
request3 :
,: ;
CancellationToken< M
cancellationTokenN _
)_ `
{ 
var 
albumDto 
= 
request 
. 
AlbumCreationDto /
;/ 0
var $
getByOwnerIdAndTitleFunc $
=% &
_unitOfWork' 2
.2 3
AlbumRepository3 B
.B C 
GetByOwnerIdAndTitleC W
;W X
await!! 
_validationService!!  
.!!  !(
AssertNavigationEntityExists!!! =
<!!= >
Album!!> C
,!!C D
User!!E I
>!!I J
(!!J K
_unitOfWork!!K V
.!!V W
UserRepository!!W e
,!!e f
albumDto!!g o
.!!o p
OwnerId!!p w
,!!w x
cancellationToken	!!y ä
)
!!ä ã
;
!!ã å
await"" 
_validationService""  
.""  !$
EnsureUserUniqueProperty""! 9
(""9 :$
getByOwnerIdAndTitleFunc"": R
,""R S
albumDto""T \
.""\ ]
OwnerId""] d
,""d e
nameof""f l
(""l m
albumDto""m u
.""u v
Title""v {
)""{ |
,""| }
albumDto	""~ Ü
.
""Ü á
Title
""á å
,
""å ç
cancellationToken
""é ü
)
""ü †
;
""† °
var$$ 
albumEntity$$ 
=$$ 
_mapper$$ !
.$$! "
Map$$" %
<$$% &
Album$$& +
>$$+ ,
($$, -
albumDto$$- 5
)$$5 6
;$$6 7
var%% 
albumDb%% 
=%% 
await%% 
_unitOfWork%% '
.%%' (
AlbumRepository%%( 7
.%%7 8
Add%%8 ;
(%%; <
albumEntity%%< G
,%%G H
cancellationToken%%I Z
)%%Z [
;%%[ \
await&& 
_unitOfWork&& 
.&& 
	SaveAsync&& #
(&&# $
cancellationToken&&$ 5
)&&5 6
;&&6 7
_logger(( 
.(( 

LogSuccess(( 
((( 
nameof(( !
(((! "
Album((" '
)((' (
,((( )
albumDb((* 1
.((1 2
Id((2 4
)((4 5
;((5 6
return)) 
_mapper)) 
.)) 
Map)) 
<)) 
AlbumDto)) #
>))# $
())$ %
albumDb))% ,
))), -
;))- .
}** 
}++ ¨;
sC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Abstractions\Services\IValidationService.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
Abstractions" .
.. /
Services/ 7
;7 8
public 
	interface 
IValidationService #
{		 
Task

 
<

 	
TEntity

	 
>

 
GetExistingEntity

 #
<

# $
TEntity

$ +
>

+ ,
(

, -
IRepository 
< 
TEntity 
> 

repository '
,' (
Guid 
entityId 
, 
CancellationToken 
cancellationToken +
,+ ,
	LogAction 
	logAction 
= 
	LogAction '
.' (
Update( .
) 
where 
TEntity 
: 

BaseEntity  
;  !
Task 
< 	
TEntity	 
> (
GetExistingEntityWithInclude .
<. /
TEntity/ 6
>6 7
(7 8
Func 
< 
Guid 
, 
CancellationToken $
,$ %

Expression& 0
<0 1
Func1 5
<5 6
TEntity6 =
,= >
object? E
>E F
>F G
[G H
]H I
,I J
TaskK O
<O P
TEntityP W
?W X
>X Y
>Y Z$
getEntityWithIncludeFunc[ s
,s t
Guid 
entityId 
, 
	LogAction 
	logAction 
, 
CancellationToken 
cancellationToken +
,+ ,
params 

Expression 
< 
Func 
< 
TEntity &
,& '
object( .
>. /
>/ 0
[0 1
]1 2
includeProperties3 D
) 
where 
TEntity 
: 

BaseEntity  
;  !
Task 
AssertEntityExists	 
< 
TEntity #
># $
($ %
IRepository 
< 
TEntity 
> 

repository '
,' (
Guid 
entityId 
, 
CancellationToken 
cancellationToken +
,+ ,
	LogAction 
	logAction 
= 
	LogAction '
.' (
Delete( .
) 
where 
TEntity 
: 

BaseEntity  
;  !
Task   (
AssertNavigationEntityExists  	 %
<  % &
TEntity  & -
,  - .
TNav  / 3
>  3 4
(  4 5
IRepository!! 
<!! 
TNav!! 
>!! 

repository!! $
,!!$ %
Guid"" 
navId"" 
,"" 
CancellationToken## 
cancellationToken## +
,##+ ,
	LogAction$$ 
	logAction$$ 
=$$ 
	LogAction$$ '
.$$' (
Create$$( .
)%% 
where%% 
TNav%% 
:%% 

BaseEntity%% 
;%% 
Task'' (
AssertNavigationEntityExists''	 %
<''% &
TEntity''& -
,''- .
TNav''/ 3
>''3 4
(''4 5
IRepository(( 
<(( 
TNav(( 
>(( 

repository(( $
,(($ %
Guid)) 
?)) 
id)) 
,)) 
CancellationToken** 
cancellationToken** +
,**+ ,
	LogAction++ 
	logAction++ 
=++ 
	LogAction++ '
.++' (
Create++( .
),, 
where,, 
TNav,, 
:,, 

BaseEntity,, 
;,, 
Task..  
EnsureUniqueProperty..	 
<.. 
TEntity.. %
,..% &
	TProperty..' 0
>..0 1
(..1 2
Func// 
<// 
	TProperty// 
,// 
CancellationToken// )
,//) *
Task//+ /
</// 0
TEntity//0 7
?//7 8
>//8 9
>//9 :
getEntityFunc//; H
,//H I
string00 
propertyName00 
,00 
	TProperty11 
propertyValue11 
,11  
CancellationToken22 
cancellationToken22 +
,22+ ,
	LogAction33 
	logAction33 
=33 
	LogAction33 '
.33' (
Create33( .
)33. /
;33/ 0
Task55 (
EnsureUniquePropertyExceptId55	 %
<55% &
TEntity55& -
,55- .
	TProperty55/ 8
>558 9
(559 :
Func66 
<66 
	TProperty66 
,66 
Guid66 
,66 
CancellationToken66 /
,66/ 0
Task661 5
<665 6
IEnumerable666 A
<66A B
TEntity66B I
>66I J
>66J K
>66K L
getEntitiesFunc66M \
,66\ ]
string77 
propertyName77 
,77 
	TProperty88 
propertyValue88 
,88  
Guid99 
entityId99 
,99 
CancellationToken:: 
cancellationToken:: +
,::+ ,
	LogAction;; 
	logAction;; 
=;; 
	LogAction;; '
.;;' (
Update;;( .
);;. /
;;;/ 0
Task== $
EnsureUserUniqueProperty==	 !
<==! "
TEntity==" )
,==) *
	TProperty==+ 4
>==4 5
(==5 6
Func>> 
<>> 
Guid>> 
,>> 
	TProperty>> 
,>> 
CancellationToken>> /
,>>/ 0
Task>>1 5
<>>5 6
TEntity>>6 =
?>>= >
>>>> ?
>>>? @
getEntityFunc>>A N
,>>N O
Guid?? 
ownerId?? 
,?? 
string@@ 
propertyName@@ 
,@@ 
	TPropertyAA 
propertyValueAA 
,AA  
CancellationTokenBB 
cancellationTokenBB +
,BB+ ,
	LogActionCC 
	logActionCC 
=CC 
	LogActionCC '
.CC' (
CreateCC( .
)CC. /
;CC/ 0
TaskEE ,
 EnsureUserUniquePropertyExceptIdEE	 )
<EE) *
TEntityEE* 1
,EE1 2
	TPropertyEE3 <
>EE< =
(EE= >
FuncFF 
<FF 
GuidFF 
,FF 
	TPropertyFF 
,FF 
GuidFF "
,FF" #
CancellationTokenFF$ 5
,FF5 6
TaskFF7 ;
<FF; <
IEnumerableFF< G
<FFG H
TEntityFFH O
>FFO P
>FFP Q
>FFQ R
getEntitiesFuncFFS b
,FFb c
GuidGG 
ownerIdGG 
,GG 
stringHH 
propertyNameHH 
,HH 
	TPropertyII 
propertyValueII 
,II  
GuidJJ 
entityIdJJ 
,JJ 
CancellationTokenKK 
cancellationTokenKK +
,KK+ ,
	LogActionLL 
	logActionLL 
=LL 
	LogActionLL '
.LL' (
UpdateLL( .
)LL. /
;LL/ 0
}MM Ì
pC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Abstractions\Services\ILoggingService.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
Abstractions" .
.. /
Services/ 7
;7 8
public 
	interface 
ILoggingService  
{ 
void 

LogSuccess	 
( 
string 

entityName %
,% &
	LogAction' 0
	logAction1 :
=; <
	LogAction= F
.F G
GetG J
)J K
;K L
void 

LogSuccess	 
( 
string 

entityName %
,% &
Guid' +
entityId, 4
,4 5
	LogAction6 ?
	logAction@ I
=J K
	LogActionL U
.U V
CreateV \
)\ ]
;] ^
void		 -
!LogAndThrowNotAuthorizedException			 *
(		* +
string		+ 1

entityName		2 <
,		< =
Guid		> B
entityId		C K
,		K L
string		M S
navName		T [
,		[ \
Guid		] a
navId		b g
,		g h
	LogAction		i r
	logAction		s |
=		} ~
	LogAction			 à
.
		à â
Update
		â è
)
		è ê
;
		ê ë
void

 (
LogAndThrowNotFoundException

	 %
(

% &
string

& ,

entityName

- 7
,

7 8
Guid

9 =
entityId

> F
,

F G
	LogAction

H Q
	logAction

R [
)

[ \
;

\ ]
void 5
)LogAndThrowNotFoundExceptionForNavigation	 2
(2 3
string3 9

entityName: D
,D E
stringF L
navNameM T
,T U
GuidV Z
navId[ `
,` a
	LogActionb k
	logActionl u
)u v
;v w
void )
LogAndThrowDuplicateException	 &
<& '
T' (
>( )
() *
string* 0

entityName1 ;
,; <
string= C
propertyNameD P
,P Q
TR S
propertyValueT a
,a b
	LogActionc l
	logActionm v
=w x
	LogAction	y Ç
.
Ç É
Update
É â
)
â ä
;
ä ã
void 0
$LogAndThrowDuplicateExceptionForUser	 -
<- .
T. /
>/ 0
(0 1
string1 7

entityName8 B
,B C
stringD J
propertyNameK W
,W X
TY Z
propertyValue[ h
,h i
Guidj n
ownerIdo v
,v w
	LogAction	x Å
	logAction
Ç ã
)
ã å
;
å ç
} ‚
tC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Abstractions\Repositories\IUserRepository.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
Abstractions" .
.. /
Repositories/ ;
;; <
public 
	interface 
IUserRepository  
:! "
IRepository# .
<. /
User/ 3
>3 4
{ 
Task 
< 	
User	 
? 
> 
GetByUsername 
( 
string $
username% -
,- .
CancellationToken/ @
cancellationTokenA R
)R S
;S T
Task 
< 	
User	 
? 
> 

GetByEmail 
( 
string !
email" '
,' (
CancellationToken) :
cancellationToken; L
)L M
;M N
Task		 
<		 	
User			 
?		 
>		  
GetByUsernameOrEmail		 $
(		$ %
string		% +
username		, 4
,		4 5
string		6 <
email		= B
,		B C
CancellationToken		D U
cancellationToken		V g
)		g h
;		h i
Task

 
<

 	
User

	 
?

 
>

 /
#GetByUsernameOrEmailWhereIdNotEqual

 3
(

3 4
string

4 :
username

; C
,

C D
string

E K
email

L Q
,

Q R
Guid

S W
userId

X ^
,

^ _
CancellationToken

` q
cancellationToken	

r É
)


É Ñ
;


Ñ Ö
} É
~C:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Abstractions\Repositories\IUserPreferenceRepository.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
Abstractions" .
.. /
Repositories/ ;
;; <
public 
	interface %
IUserPreferenceRepository *
:+ ,
IRepository- 8
<8 9
UserPreference9 G
>G H
{ 
} ø
tC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Abstractions\Repositories\ISongRepository.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
Abstractions" .
.. /
Repositories/ ;
;; <
public 
	interface 
ISongRepository  
:! "
IRepository# .
<. /
Song/ 3
>3 4
{ 
Task 
DeleteWhere	 
( 

Expression 
<  
Func  $
<$ %
Song% )
,) *
bool+ /
>/ 0
>0 1
	predicate2 ;
,; <
CancellationToken= N
cancellationTokenO `
)` a
;a b
Task		 
<		 	
Song			 
?		 
>		  
GetByOwnerIdAndTitle		 $
(		$ %
Guid		% )
ownerId		* 1
,		1 2
string		3 9
title		: ?
,		? @
CancellationToken		A R
cancellationToken		S d
)		d e
;		e f
Task

 
<

 	
IEnumerable

	 
<

 
Song

 
>

 
>

 /
#GetByOwnerIdAndTitleWhereIdNotEqual

 ?
(

? @
Guid

@ D
ownerId

E L
,

L M
string

N T
title

U Z
,

Z [
Guid

\ `
songId

a g
,

g h
CancellationToken

i z
cancellationToken	

{ å
)


å ç
;


ç é
} é
pC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Abstractions\Repositories\IRepository.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
Abstractions" .
.. /
Repositories/ ;
;; <
public 
	interface 
IRepository 
< 
T 
> 
where  %
T& '
:( )

BaseEntity* 4
{ 
Task 
< 	
T	 

?
 
> 
GetById 
( 
Guid 
id 
, 
CancellationToken /
cancellationToken0 A
)A B
;B C
Task		 
<		 	
T			 

?		
 
>		 
GetByIdWithInclude		 
(		  
Guid		  $
id		% '
,		' (
CancellationToken		) :
cancellationToken		; L
,		L M
params		N T

Expression		U _
<		_ `
Func		` d
<		d e
T		e f
,		f g
object		h n
>		n o
>		o p
[		p q
]		q r
includeProperties			s Ñ
)
		Ñ Ö
;
		Ö Ü
Task

 
<

 	
List

	 
<

 
T

 
>

 
>

 
GetAll

 
(

 
CancellationToken

 *
cancellationToken

+ <
)

< =
;

= >
Task 
< 	
T	 

>
 
Add 
( 
T 
entity 
, 
CancellationToken +
cancellationToken, =
)= >
;> ?
Task 
Delete	 
( 
Guid 
id 
, 
CancellationToken *
cancellationToken+ <
)< =
;= >
Task 
< 	
T	 

>
 
Update 
( 
T 
entity 
, 
CancellationToken .
cancellationToken/ @
)@ A
;A B
} Á
uC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Abstractions\Repositories\IGenreRepository.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
Abstractions" .
.. /
Repositories/ ;
;; <
public 
	interface 
IGenreRepository !
:" #
IRepository$ /
</ 0
Genre0 5
>5 6
{ 
Task 
< 	
Genre	 
? 
> 
	GetByName 
( 
string !
name" &
,& '
CancellationToken( 9
cancellationToken: K
)K L
;L M
Task 
< 	
IEnumerable	 
< 
Genre 
> 
> $
GetByNameWhereIdNotEqual 5
(5 6
string6 <
name= A
,A B
GuidC G
genreIdH O
,O P
CancellationTokenQ b
cancellationTokenc t
)t u
;u v
}		 ˚
uC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Abstractions\Repositories\IAlbumRepository.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
Abstractions" .
.. /
Repositories/ ;
;; <
public 
	interface 
IAlbumRepository !
:" #
IRepository$ /
</ 0
Album0 5
>5 6
{ 
Task 
< 	
Album	 
? 
> 

GetByTitle 
( 
string "
title# (
,( )
CancellationToken* ;
cancellationToken< M
)M N
;N O
Task 
< 	
Album	 
? 
>  
GetByOwnerIdAndTitle %
(% &
Guid& *
ownerId+ 2
,2 3
string4 :
title; @
,@ A
CancellationTokenB S
cancellationTokenT e
)e f
;f g
Task		 
<		 	
IEnumerable			 
<		 
Album		 
>		 
>		 /
#GetByOwnerIdAndTitleWhereIdNotEqual		 @
(		@ A
Guid		A E
ownerId		F M
,		M N
string		O U
title		V [
,		[ \
Guid		] a
albumId		b i
,		i j
CancellationToken		k |
cancellationToken			} é
)
		é è
;
		è ê
}

 Î
pC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Abstractions\Mapping\IMappingProvider.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
Abstractions" .
.. /
Mapping/ 6
;6 7
public 
	interface 
IMappingProvider !
{ 
TDestination 
Map 
< 
TDestination !
>! "
(" #
object# )
?) *
source+ 1
)1 2
;2 3
TDestination 
Map 
< 
TSource 
, 
TDestination *
>* +
(+ ,
TSource, 3
source4 :
,: ;
TDestination< H
destinationI T
)T U
;U V
} Ø
pC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Abstractions\Logging\ILoggingProvider.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
Abstractions" .
.. /
Logging/ 6
;6 7
public 
	interface 
ILoggingProvider !
{ 
void 
LogInformation	 
( 
string 
message &
)& '
;' (
void 
LogInformation	 
( 
string 
messageTemplate .
,. /
params0 6
object7 =
?= >
[> ?
]? @
?@ A
propertyValuesB P
)P Q
;Q R
void 
LogInformation	 
( 
	Exception !
?! "
	exception# ,
,, -
string. 4
messageTemplate5 D
,D E
paramsF L
objectM S
?S T
[T U
]U V
?V W
propertyValuesX f
)f g
;g h
void 

LogWarning	 
( 
string 
message "
)" #
;# $
void		 

LogWarning			 
(		 
string		 
messageTemplate		 *
,		* +
params		, 2
object		3 9
?		9 :
[		: ;
]		; <
?		< =
propertyValues		> L
)		L M
;		M N
void

 

LogWarning

	 
(

 
	Exception

 
?

 
	exception

 (
,

( )
string

* 0
messageTemplate

1 @
,

@ A
params

B H
object

I O
?

O P
[

P Q
]

Q R
?

R S
propertyValues

T b
)

b c
;

c d
void 
LogError	 
( 
string 
message  
,  !
	Exception" +
ex, .
). /
;/ 0
void 
LogError	 
( 
string 
messageTemplate (
,( )
params* 0
object1 7
?7 8
[8 9
]9 :
?: ;
propertyValues< J
)J K
;K L
void 
LogError	 
( 
	Exception 
? 
	exception &
,& '
string( .
messageTemplate/ >
,> ?
params@ F
objectG M
?M N
[N O
]O P
?P Q
propertyValuesR `
)` a
;a b
} ñ
cC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Application\Abstractions\IUnitOfWork.cs
	namespace 	
Streamphony
 
. 
Application !
.! "
Abstractions" .
;. /
public 
	interface 
IUnitOfWork 
{ 
Task 
	SaveAsync	 
( 
CancellationToken $
cancellationToken% 6
)6 7
;7 8
Task !
BeginTransactionAsync	 
( 
CancellationToken 0
cancellationToken1 B
)B C
;C D
Task		 "
CommitTransactionAsync			 
(		  
CancellationToken		  1
cancellationToken		2 C
)		C D
;		D E
Task

 $
RollbackTransactionAsync

	 !
(

! "
CancellationToken

" 3
cancellationToken

4 E
)

E F
;

F G
public 

ISongRepository 
SongRepository )
{* +
get, /
;/ 0
}1 2
public 

IUserRepository 
UserRepository )
{* +
get, /
;/ 0
}1 2
public 

IAlbumRepository 
AlbumRepository +
{, -
get. 1
;1 2
}3 4
public 

IGenreRepository 
GenreRepository +
{, -
get. 1
;1 2
}3 4
public 
%
IUserPreferenceRepository $$
UserPreferenceRepository% =
{> ?
get@ C
;C D
}E F
} 