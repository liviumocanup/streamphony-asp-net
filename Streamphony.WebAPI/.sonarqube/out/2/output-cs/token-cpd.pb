∞
ÉC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Validators\DTOs\UserPreferenceDtoValidator.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0

Validators0 :
.: ;
DTOs; ?
;? @
public 
class &
UserPreferenceDtoValidator '
:( )
AbstractValidator* ;
<; <
UserPreferenceDto< M
>M N
{ 
public 
&
UserPreferenceDtoValidator %
(% &
)& '
{		 
RuleFor

 
(

 
userPreference

 
=>

 !
userPreference

" 0
.

0 1
Language

1 9
)

9 :
. 
MaximumLength 
( 
$num 
) 
; 
} 
} ï
yC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Validators\DTOs\UserDtoValidator.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0

Validators0 :
.: ;
DTOs; ?
;? @
public 
class 
UserDtoValidator 
: 
AbstractValidator  1
<1 2
UserDto2 9
>9 :
{ 
public		 

UserDtoValidator		 
(		 
)		 
{

 
Include 
( 
new $
UserCreationDtoValidator ,
(, -
)- .
). /
;/ 0
} 
} ï
yC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Validators\DTOs\SongDtoValidator.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0

Validators0 :
.: ;
DTOs; ?
;? @
public 
class 
SongDtoValidator 
: 
AbstractValidator  1
<1 2
SongDto2 9
>9 :
{ 
public		 

SongDtoValidator		 
(		 
)		 
{

 
Include 
( 
new $
SongCreationDtoValidator ,
(, -
)- .
). /
;/ 0
} 
} ö
zC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Validators\DTOs\GenreDtoValidator.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0

Validators0 :
.: ;
DTOs; ?
;? @
public 
class 
GenreDtoValidator 
:  
AbstractValidator! 2
<2 3
GenreDto3 ;
>; <
{ 
public		 

GenreDtoValidator		 
(		 
)		 
{

 
Include 
( 
new %
GenreCreationDtoValidator -
(- .
). /
)/ 0
;0 1
} 
} ö
zC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Validators\DTOs\AlbumDtoValidator.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0

Validators0 :
.: ;
DTOs; ?
;? @
public 
class 
AlbumDtoValidator 
:  
AbstractValidator! 2
<2 3
AlbumDto3 ;
>; <
{ 
public		 

AlbumDtoValidator		 
(		 
)		 
{

 
Include 
( 
new %
AlbumCreationDtoValidator -
(- .
). /
)/ 0
;0 1
} 
} ∆
âC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Validators\CreationDTOs\UserCreationDtoValidator.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0

Validators0 :
.: ;
CreationDTOs; G
;G H
public 
class $
UserCreationDtoValidator %
:& '
AbstractValidator( 9
<9 :
UserCreationDto: I
>I J
{ 
public		 
$
UserCreationDtoValidator		 #
(		# $
)		$ %
{

 
RuleFor 
( 
user 
=> 
user 
. 
Username %
)% &
. 
NotEmpty 
( 
) 
. 
MaximumLength 
( 
$num 
) 
; 
RuleFor 
( 
user 
=> 
user 
. 
Email "
)" #
. 
NotEmpty 
( 
) 
. 
MaximumLength 
( 
$num 
) 
. 
EmailAddress 
( 
) 
; 
RuleFor 
( 
user 
=> 
user 
. 

ArtistName '
)' (
. 
NotEmpty 
( 
) 
. 
MaximumLength 
( 
$num 
) 
;  
RuleFor 
( 
user 
=> 
user 
. 
DateOfBirth (
)( )
. 
DateNotInFuture 
( 
) 
; 
RuleFor 
( 
user 
=> 
user 
. 
ProfilePictureUrl .
). /
. 
MaximumLength 
( 
$num 
)  
. 
ValidUrl 
( 
) 
; 
} 
} é
âC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Validators\CreationDTOs\SongCreationDtoValidator.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0

Validators0 :
.: ;
CreationDTOs; G
;G H
public 
class $
SongCreationDtoValidator %
:& '
AbstractValidator( 9
<9 :
SongCreationDto: I
>I J
{ 
public		 
$
SongCreationDtoValidator		 #
(		# $
)		$ %
{

 
RuleFor 
( 
song 
=> 
song 
. 
Title "
)" #
. 
NotEmpty 
( 
) 
. 
MaximumLength 
( 
$num 
) 
; 
RuleFor 
( 
song 
=> 
song 
. 
Duration %
)% &
. 
Must 
( 
duration 
=> 
duration &
>' (
TimeSpan) 1
.1 2
Zero2 6
)6 7
. 
WithMessage 
( 
$str >
)> ?
;? @
RuleFor 
( 
song 
=> 
song 
. 
Url  
)  !
. 
NotEmpty 
( 
) 
. 
MaximumLength 
( 
$num 
)  
. 
ValidUrl 
( 
) 
; 
} 
} —
âC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Validators\CreationDTOs\GenreCeationDtoValidator.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0

Validators0 :
.: ;
CreationDTOs; G
;G H
public 
class %
GenreCreationDtoValidator &
:' (
AbstractValidator) :
<: ;
GenreCreationDto; K
>K L
{ 
public 
%
GenreCreationDtoValidator $
($ %
)% &
{		 
RuleFor

 
(

 
genre

 
=>

 
genre

 
.

 
Name

 #
)

# $
. 
NotEmpty 
( 
) 
. 
MaximumLength 
( 
$num 
) 
. 
Matches 
( 
$str %
)% &
.& '
WithMessage' 2
(2 3
$str3 e
)e f
;f g
RuleFor 
( 
genre 
=> 
genre 
. 
Description *
)* +
. 
NotEmpty 
( 
) 
. 
MaximumLength 
( 
$num 
)  
;  !
} 
} ©

äC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Validators\CreationDTOs\AlbumCreationDtoValidator.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0

Validators0 :
.: ;
CreationDTOs; G
;G H
public 
class %
AlbumCreationDtoValidator &
:' (
AbstractValidator) :
<: ;
AlbumCreationDto; K
>K L
{ 
public		 
%
AlbumCreationDtoValidator		 $
(		$ %
)		% &
{

 
RuleFor 
( 
album 
=> 
album 
. 
Title $
)$ %
. 
NotEmpty 
( 
) 
. 
MaximumLength 
( 
$num 
) 
; 
RuleFor 
( 
album 
=> 
album 
. 
CoverImageUrl ,
), -
. 
MaximumLength 
( 
$num 
)  
. 
ValidUrl 
( 
) 
; 
} 
} «
}C:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Configurations\UserPreferencesConfig.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0
Configurations0 >
;> ?
public 
class !
UserPreferencesConfig "
:# $$
IEntityTypeConfiguration% =
<= >
UserPreference> L
>L M
{ 
public		 

void		 
	Configure		 
(		 
EntityTypeBuilder		 +
<		+ ,
UserPreference		, :
>		: ;
builder		< C
)		C D
{

 
builder 
. 
HasKey 
( 
up 
=> 
up 
.  
Id  "
)" #
;# $
builder 
. 
HasOne 
( 
up 
=> 
up 
.  
User  $
)$ %
. 
WithOne 
( 
u 
=> 
u 
. 
Preferences '
)' (
. 
HasForeignKey 
< 
UserPreference )
>) *
(* +
up+ -
=>. 0
up1 3
.3 4
Id4 6
)6 7
. 
OnDelete 
( 
DeleteBehavior $
.$ %
Cascade% ,
), -
;- .
} 
} ª
rC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Configurations\UserConfig.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0
Configurations0 >
;> ?
public 
class 

UserConfig 
: $
IEntityTypeConfiguration 2
<2 3
User3 7
>7 8
{ 
public		 

void		 
	Configure		 
(		 
EntityTypeBuilder		 +
<		+ ,
User		, 0
>		0 1
builder		2 9
)		9 :
{

 
builder 
. 
Property 
( 
u 
=> 
u 
.  
Username  (
)( )
.) *

IsRequired* 4
(4 5
)5 6
.6 7
HasMaxLength7 C
(C D
$numD F
)F G
;G H
builder 
. 
HasIndex 
( 
u 
=> 
u 
.  
Username  (
)( )
.) *
IsUnique* 2
(2 3
)3 4
;4 5
builder 
. 
Property 
( 
u 
=> 
u 
.  
Email  %
)% &
.& '

IsRequired' 1
(1 2
)2 3
.3 4
HasMaxLength4 @
(@ A
$numA D
)D E
;E F
builder 
. 
HasIndex 
( 
u 
=> 
u 
.  
Email  %
)% &
.& '
IsUnique' /
(/ 0
)0 1
;1 2
builder 
. 
Property 
( 
u 
=> 
u 
.  

ArtistName  *
)* +
.+ ,

IsRequired, 6
(6 7
)7 8
.8 9
HasMaxLength9 E
(E F
$numF I
)I J
;J K
builder 
. 
Property 
( 
u 
=> 
u 
.  
ProfilePictureUrl  1
)1 2
.2 3
HasMaxLength3 ?
(? @
$num@ D
)D E
;E F
builder 
. 
HasMany 
( 
u 
=> 
u 
. 
UploadedSongs ,
), -
. 
WithOne 
( 
s 
=> 
s 
.  
Owner  %
)% &
. 
HasForeignKey 
( 
s  
=>! #
s$ %
.% &
OwnerId& -
)- .
. 
OnDelete 
( 
DeleteBehavior (
.( )
NoAction) 1
)1 2
;2 3
builder 
. 
HasMany 
( 
u 
=> 
u 
. 
OwnedAlbums *
)* +
. 
WithOne 
( 
a 
=> 
a 
.  
Owner  %
)% &
. 
HasForeignKey 
( 
a  
=>! #
a$ %
.% &
OwnerId& -
)- .
. 
OnDelete 
( 
DeleteBehavior (
.( )
NoAction) 1
)1 2
;2 3
} 
} ë
rC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Configurations\SongConfig.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0
Configurations0 >
;> ?
public 
class 

SongConfig 
: $
IEntityTypeConfiguration 2
<2 3
Song3 7
>7 8
{ 
public		 
void		 
	Configure		 
(		 
EntityTypeBuilder		 /
<		/ 0
Song		0 4
>		4 5
builder		6 =
)		= >
{

 	
builder 
. 
Property  
(  !
s! "
=># %
s& '
.' (
Title( -
)- .
.. /

IsRequired/ 9
(9 :
): ;
.; <
HasMaxLength< H
(H I
$numI K
)K L
;L M
builder 
. 
HasIndex  
(  !
s! "
=># %
new& )
{* +
s, -
.- .
Title. 3
,3 4
s5 6
.6 7
OwnerId7 >
}? @
)@ A
.A B
IsUniqueB J
(J K
)K L
;L M
builder 
. 
Property  
(  !
s! "
=># %
s& '
.' (
Duration( 0
)0 1
.1 2

IsRequired2 <
(< =
)= >
;> ?
builder 
. 
Property  
(  !
a! "
=># %
a& '
.' (
Url( +
)+ ,
., -

IsRequired- 7
(7 8
)8 9
.9 :
HasMaxLength: F
(F G
$numG K
)K L
;L M
builder 
. 
HasOne 
( 
s  
=>! #
s$ %
.% &
Owner& +
)+ ,
. 
WithMany !
(! "
u" #
=>$ &
u' (
.( )
UploadedSongs) 6
)6 7
. 
HasForeignKey &
(& '
s' (
=>) +
s, -
.- .
OwnerId. 5
)5 6
. 
OnDelete !
(! "
DeleteBehavior" 0
.0 1
Restrict1 9
)9 :
;: ;
builder 
. 
HasOne 
( 
s  
=>! #
s$ %
.% &
Genre& +
)+ ,
. 
WithMany !
(! "
g" #
=>$ &
g' (
.( )
Songs) .
). /
. 
HasForeignKey &
(& '
s' (
=>) +
s, -
.- .
GenreId. 5
)5 6
. 
OnDelete !
(! "
DeleteBehavior" 0
.0 1
SetNull1 8
)8 9
;9 :
builder 
. 
HasOne 
( 
s  
=>! #
s$ %
.% &
Album& +
)+ ,
. 
WithMany !
(! "
a" #
=>$ &
a' (
.( )
Songs) .
). /
. 
HasForeignKey &
(& '
s' (
=>) +
s, -
.- .
AlbumId. 5
)5 6
. 
OnDelete !
(! "
DeleteBehavior" 0
.0 1
SetNull1 8
)8 9
;9 :
} 	
} Ÿ
sC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Configurations\GenreConfig.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0
Configurations0 >
;> ?
public 
class 
GenreConfig 
: $
IEntityTypeConfiguration 3
<3 4
Genre4 9
>9 :
{ 
public		 

void		 
	Configure		 
(		 
EntityTypeBuilder		 +
<		+ ,
Genre		, 1
>		1 2
builder		3 :
)		: ;
{

 
builder 
. 
Property 
( 
g 
=> 
g 
.  
Name  $
)$ %
.% &

IsRequired& 0
(0 1
)1 2
.2 3
HasMaxLength3 ?
(? @
$num@ B
)B C
;C D
builder 
. 
HasIndex 
( 
g 
=> 
g 
.  
Name  $
)$ %
.% &
IsUnique& .
(. /
)/ 0
;0 1
builder 
. 
Property 
( 
g 
=> 
g 
.  
Description  +
)+ ,
., -

IsRequired- 7
(7 8
)8 9
.9 :
HasMaxLength: F
(F G
$numG K
)K L
;L M
builder 
. 
HasMany 
( 
g 
=> 
g 
. 
Songs $
)$ %
. 
WithOne 
( 
s 
=> 
s 
.  
Genre  %
)% &
. 
HasForeignKey 
( 
s  
=>! #
s$ %
.% &
GenreId& -
)- .
. 
OnDelete 
( 
DeleteBehavior (
.( )
NoAction) 1
)1 2
;2 3
} 
} Ù
sC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Configurations\AlbumConfig.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0
Configurations0 >
;> ?
public 
class 
AlbumConfig 
: $
IEntityTypeConfiguration 3
<3 4
Album4 9
>9 :
{ 
public		 

void		 
	Configure		 
(		 
EntityTypeBuilder		 +
<		+ ,
Album		, 1
>		1 2
builder		3 :
)		: ;
{

 
builder 
. 
Property 
( 
a 
=> 
a 
.  
Title  %
)% &
.& '

IsRequired' 1
(1 2
)2 3
.3 4
HasMaxLength4 @
(@ A
$numA C
)C D
;D E
builder 
. 
HasIndex 
( 
a 
=> 
new !
{" #
a$ %
.% &
Title& +
,+ ,
a- .
.. /
OwnerId/ 6
}7 8
)8 9
.9 :
IsUnique: B
(B C
)C D
;D E
builder 
. 
Property 
( 
a 
=> 
a 
.  
CoverImageUrl  -
)- .
.. /
HasMaxLength/ ;
(; <
$num< @
)@ A
;A B
builder 
. 
HasOne 
( 
a 
=> 
a 
. 
Owner #
)# $
. 
WithMany 
( 
u 
=> 
u  
.  !
OwnedAlbums! ,
), -
. 
HasForeignKey 
( 
a  
=>! #
a$ %
.% &
OwnerId& -
)- .
. 
OnDelete 
( 
DeleteBehavior (
.( )
Cascade) 0
)0 1
;1 2
builder 
. 
HasMany 
( 
a 
=> 
a 
. 
Songs $
)$ %
. 
WithOne 
( 
s 
=> 
s 
.  
Album  %
)% &
. 
HasForeignKey 
( 
s  
=>! #
s$ %
.% &
AlbumId& -
)- .
. 
OnDelete 
( 
DeleteBehavior (
.( )
NoAction) 1
)1 2
;2 3
} 
} æ
yC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Validation\Configurations\AlbumArtistConfig.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Validation% /
./ 0
Configurations0 >
;> ?
public 
class 
AlbumArtistConfig 
:  $
IEntityTypeConfiguration! 9
<9 :
AlbumArtist: E
>E F
{ 
public		 

void		 
	Configure		 
(		 
EntityTypeBuilder		 +
<		+ ,
AlbumArtist		, 7
>		7 8
builder		9 @
)		@ A
{

 
builder 
. 
Property 
( 
aa 
=> 
aa !
.! "
Role" &
)& '
.' (
HasConversion( 5
<5 6
string6 <
>< =
(= >
)> ?
.? @
HasMaxLength@ L
(L M
$numM O
)O P
;P Q
builder 
. 
HasKey 
( 
aa 
=> 
new  
{! "
aa# %
.% &
AlbumId& -
,- .
aa/ 1
.1 2
ArtistId2 :
,: ;
aa< >
.> ?
Role? C
}D E
)E F
;F G
builder 
. 
HasOne 
( 
aa 
=> 
aa 
.  
Album  %
)% &
. 
WithMany 
( 
a 
=> 
a  
.  !
Artists! (
)( )
. 
HasForeignKey 
( 
aa !
=>" $
aa% '
.' (
AlbumId( /
)/ 0
. 
OnDelete 
( 
DeleteBehavior (
.( )
Cascade) 0
)0 1
;1 2
builder 
. 
HasOne 
( 
aa 
=> 
aa 
.  
Artist  &
)& '
. 
WithMany 
( 
u 
=> 
u  
.  !
AlbumContributions! 3
)3 4
. 
HasForeignKey 
( 
aa !
=>" $
aa% '
.' (
ArtistId( 0
)0 1
. 
OnDelete 
( 
DeleteBehavior (
.( )
Restrict) 1
)1 2
;2 3
} 
} ˛
uC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Persistence\Repositories\UserRepository.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %
Persistence% 0
.0 1
Repositories1 =
;= >
public 
class 
UserRepository 
(  
ApplicationDbContext 0
context1 8
)8 9
:: ;

Repository< F
<F G
UserG K
>K L
(L M
contextM T
)T U
,U V
IUserRepositoryW f
{		 
private

 
readonly

  
ApplicationDbContext

 )
_context

* 2
=

3 4
context

5 <
;

< =
public 

async 
Task 
< 
User 
? 
> 
GetByUsername *
(* +
string+ 1
username2 :
,: ;
CancellationToken< M
cancellationTokenN _
)_ `
{ 
return 
await 
_context 
. 
Users #
.# $
FirstOrDefaultAsync$ 7
(7 8
user8 <
=>= ?
user@ D
.D E
UsernameE M
==N P
usernameQ Y
,Y Z
cancellationToken[ l
)l m
;m n
} 
public 

async 
Task 
< 
User 
? 
> 

GetByEmail '
(' (
string( .
email/ 4
,4 5
CancellationToken6 G
cancellationTokenH Y
)Y Z
{ 
return 
await 
_context 
. 
Users #
.# $
FirstOrDefaultAsync$ 7
(7 8
user8 <
=>= ?
user@ D
.D E
EmailE J
==K M
emailN S
,S T
cancellationTokenU f
)f g
;g h
} 
public 

async 
Task 
< 
User 
? 
>  
GetByUsernameOrEmail 1
(1 2
string2 8
username9 A
,A B
stringC I
emailJ O
,O P
CancellationTokenQ b
cancellationTokenc t
)t u
{ 
return 
await 
_context 
. 
Users #
.# $
FirstOrDefaultAsync$ 7
(7 8
user8 <
=>= ?
user@ D
.D E
UsernameE M
==N P
usernameQ Y
||Z \
user] a
.a b
Emailb g
==h j
emailk p
,p q
cancellationToken	r É
)
É Ñ
;
Ñ Ö
} 
public 

async 
Task 
< 
User 
? 
> /
#GetByUsernameOrEmailWhereIdNotEqual @
(@ A
stringA G
usernameH P
,P Q
stringR X
emailY ^
,^ _
Guid` d
userIde k
,k l
CancellationTokenm ~
cancellationToken	 ê
)
ê ë
{ 
return 
await 
_context 
. 
Users #
.# $
FirstOrDefaultAsync$ 7
(7 8
user8 <
=>= ?
(@ A
userA E
.E F
UsernameF N
==O Q
usernameR Z
||[ ]
user^ b
.b c
Emailc h
==i k
emaill q
)q r
&&s u
userv z
.z {
Id{ }
!=	~ Ä
userId
Å á
,
á à
cancellationToken
â ö
)
ö õ
;
õ ú
} 
} ø
C:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Persistence\Repositories\UserPreferenceRepository.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %
Persistence% 0
.0 1
Repositories1 =
;= >
public 
class $
UserPreferenceRepository %
(% & 
ApplicationDbContext& :
context; B
)B C
:D E

RepositoryF P
<P Q
UserPreferenceQ _
>_ `
(` a
contexta h
)h i
,i j&
IUserPreferenceRepository	k Ñ
{ 
}		 Ù
qC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Persistence\Repositories\UnitOfWork.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %
Persistence% 0
.0 1
Repositories1 =
;= >
public 
class 

UnitOfWork 
: 
IUnitOfWork %
{ 
private		 
readonly		  
ApplicationDbContext		 )
_context		* 2
;		2 3
public 


UnitOfWork 
(  
ApplicationDbContext *
context+ 2
,2 3
IUserRepository4 C
userRepositoryD R
,R S
ISongRepository 
songRepository &
,& '
IAlbumRepository( 8
albumRepository9 H
,H I
IGenreRepository 
genreRepository (
,( )%
IUserPreferenceRepository* C$
userPreferenceRepositoryD \
)\ ]
{ 
_context 
= 
context 
; 
UserRepository 
= 
userRepository '
;' (
SongRepository 
= 
songRepository '
;' (
AlbumRepository 
= 
albumRepository )
;) *
GenreRepository 
= 
genreRepository )
;) *$
UserPreferenceRepository  
=! "$
userPreferenceRepository# ;
;; <
} 
public 

async 
Task 
	SaveAsync 
(  
CancellationToken  1
cancellationToken2 C
)C D
{ 
await 
_context 
. 
SaveChangesAsync '
(' (
cancellationToken( 9
)9 :
;: ;
} 
public 

async 
Task !
BeginTransactionAsync +
(+ ,
CancellationToken, =
cancellationToken> O
)O P
{ 
await 
_context 
. 
Database 
.  !
BeginTransactionAsync  5
(5 6
cancellationToken6 G
)G H
;H I
} 
public!! 

async!! 
Task!! "
CommitTransactionAsync!! ,
(!!, -
CancellationToken!!- >
cancellationToken!!? P
)!!P Q
{"" 
await## 
_context## 
.## 
Database## 
.##  "
CommitTransactionAsync##  6
(##6 7
cancellationToken##7 H
)##H I
;##I J
}$$ 
public&& 

async&& 
Task&& $
RollbackTransactionAsync&& .
(&&. /
CancellationToken&&/ @
cancellationToken&&A R
)&&R S
{'' 
await(( 
_context(( 
.(( 
Database(( 
.((  $
RollbackTransactionAsync((  8
(((8 9
cancellationToken((9 J
)((J K
;((K L
})) 
public++ 

IUserRepository++ 
UserRepository++ )
{++* +
get++, /
;++/ 0
}++1 2
public,, 

ISongRepository,, 
SongRepository,, )
{,,* +
get,,, /
;,,/ 0
},,1 2
public-- 

IAlbumRepository-- 
AlbumRepository-- +
{--, -
get--. 1
;--1 2
}--3 4
public.. 

IGenreRepository.. 
GenreRepository.. +
{.., -
get... 1
;..1 2
}..3 4
public// 
%
IUserPreferenceRepository// $$
UserPreferenceRepository//% =
{//> ?
get//@ C
;//C D
}//E F
}00 Û
uC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Persistence\Repositories\SongRepository.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %
Persistence% 0
.0 1
Repositories1 =
;= >
public		 
class		 
SongRepository		 
(		  
ApplicationDbContext		 0
context		1 8
)		8 9
:		: ;

Repository		< F
<		F G
Song		G K
>		K L
(		L M
context		M T
)		T U
,		U V
ISongRepository		W f
{

 
private 
readonly  
ApplicationDbContext )
_context* 2
=3 4
context5 <
;< =
public 

async 
Task 
DeleteWhere !
(! "

Expression" ,
<, -
Func- 1
<1 2
Song2 6
,6 7
bool8 <
>< =
>= >
	predicate? H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ 
var 
songs 
= 
_context 
. 
Songs "
." #
Where# (
(( )
	predicate) 2
)2 3
;3 4
_context 
. 
Songs 
. 
RemoveRange "
(" #
songs# (
)( )
;) *
await 
_context 
. 
SaveChangesAsync '
(' (
cancellationToken( 9
)9 :
;: ;
} 
public 

async 
Task 
< 
Song 
? 
>  
GetByOwnerIdAndTitle 1
(1 2
Guid2 6
ownerId7 >
,> ?
string@ F
titleG L
,L M
CancellationTokenN _
cancellationToken` q
)q r
{ 
return 
await 
_context 
. 
Songs #
.# $
FirstOrDefaultAsync$ 7
(7 8
song8 <
=>= ?
song@ D
.D E
OwnerIdE L
==M O
ownerIdP W
&&X Z
song[ _
._ `
Title` e
==f h
titlei n
,n o
cancellationToken	p Å
)
Å Ç
;
Ç É
} 
public 

async 
Task 
< 
IEnumerable !
<! "
Song" &
>& '
>' (/
#GetByOwnerIdAndTitleWhereIdNotEqual) L
(L M
GuidM Q
ownerIdR Y
,Y Z
string[ a
titleb g
,g h
Guidi m
songIdn t
,t u
CancellationToken	v á
cancellationToken
à ô
)
ô ö
{ 
return 
await 
_context 
. 
Songs #
.# $
Where$ )
() *
song* .
=>/ 1
song2 6
.6 7
OwnerId7 >
==? A
ownerIdB I
&&J L
songM Q
.Q R
TitleR W
==X Z
title[ `
&&a c
songd h
.h i
Idi k
!=l n
songIdo u
)u v
.v w
ToListAsync	w Ç
(
Ç É
cancellationToken
É î
)
î ï
;
ï ñ
} 
} ‡6
qC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Persistence\Repositories\Repository.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %
Persistence% 0
.0 1
Repositories1 =
;= >
public		 
class		 

Repository		 
<		 
T		 
>		 
(		  
ApplicationDbContext		 /
context		0 7
)		7 8
:		9 :
IRepository		; F
<		F G
T		G H
>		H I
where		J O
T		P Q
:		R S

BaseEntity		T ^
{

 
private 
readonly  
ApplicationDbContext )
_context* 2
=3 4
context5 <
;< =
public 

async 
Task 
< 
List 
< 
T 
> 
> 
GetAll %
(% &
CancellationToken& 7
cancellationToken8 I
)I J
{ 
return 
await 
_context 
. 
Set !
<! "
T" #
># $
($ %
)% &
.& '
ToListAsync' 2
(2 3
cancellationToken3 D
)D E
;E F
} 
public 

async 
Task 
< 
T 
? 
> 
GetById !
(! "
Guid" &
id' )
,) *
CancellationToken+ <
cancellationToken= N
)N O
{ 
return 
await 
_context 
. 
	FindAsync '
<' (
T( )
>) *
(* +
[+ ,
id, .
]. /
,/ 0
cancellationToken1 B
:B C
cancellationTokenD U
)U V
;V W
} 
public 

async 
Task 
< 
T 
? 
> 
GetByIdWithInclude ,
(, -
Guid- 1
id2 4
,4 5
CancellationToken6 G
cancellationTokenH Y
,Y Z
params[ a

Expressionb l
<l m
Funcm q
<q r
Tr s
,s t
objectu {
>{ |
>| }
[} ~
]~ 
includeProperties
Ä ë
)
ë í
{ 
var 
query 
= 
IncludeProperties %
(% &
includeProperties& 7
)7 8
;8 9
return 
await 
query 
. 
FirstOrDefaultAsync .
(. /
entity/ 5
=>6 8
entity9 ?
.? @
Id@ B
==C E
idF H
,H I
cancellationTokenJ [
)[ \
;\ ]
} 
public 

async 
Task 
< 
T 
> 
Add 
( 
T 
entity %
,% &
CancellationToken' 8
cancellationToken9 J
)J K
{ 
_context 
. 
Set 
< 
T 
> 
( 
) 
. 
Add 
( 
entity $
)$ %
;% &
await   
_context   
.   
SaveChangesAsync   '
(  ' (
cancellationToken  ( 9
)  9 :
;  : ;
return!! 
entity!! 
;!! 
}"" 
public$$ 

async$$ 
Task$$ 
Delete$$ 
($$ 
Guid$$ !
id$$" $
,$$$ %
CancellationToken$$& 7
cancellationToken$$8 I
)$$I J
{%% 
var&& 
entity&& 
=&& 
await&& 
_context&& #
.&&# $
Set&&$ '
<&&' (
T&&( )
>&&) *
(&&* +
)&&+ ,
.&&, -
	FindAsync&&- 6
(&&6 7
[&&7 8
id&&8 :
]&&: ;
,&&; <
cancellationToken&&= N
:&&N O
cancellationToken&&P a
)&&a b
??&&c e
throw'' 
new'' !
ArgumentException''" 3
(''3 4
$"''4 6
$str''6 E
{''E F
typeof''F L
(''L M
T''M N
)''N O
}''O P
$str''P Y
{''Y Z
id''Z \
}''\ ]
$str''] g
"''g h
)''h i
;''i j
_context)) 
.)) 
Set)) 
<)) 
T)) 
>)) 
()) 
))) 
.)) 
Remove))  
())  !
entity))! '
)))' (
;))( )
await** 
_context** 
.** 
SaveChangesAsync** '
(**' (
cancellationToken**( 9
)**9 :
;**: ;
}++ 
public-- 

async-- 
Task-- 
<-- 
T-- 
>-- 
Update-- 
(--  
T--  !
entity--" (
,--( )
CancellationToken--* ;
cancellationToken--< M
)--M N
{.. 
_context// 
.// 
Set// 
<// 
T// 
>// 
(// 
)// 
.// 
Update//  
(//  !
entity//! '
)//' (
;//( )
await00 
_context00 
.00 
SaveChangesAsync00 '
(00' (
cancellationToken00( 9
)009 :
;00: ;
return11 
entity11 
;11 
}22 
private44 

IQueryable44 
<44 
T44 
>44 
IncludeProperties44 +
(44+ ,
params44, 2

Expression443 =
<44= >
Func44> B
<44B C
T44C D
,44D E
object44F L
>44L M
>44M N
[44N O
]44O P
includeProperties44Q b
)44b c
{55 

IQueryable66 
<66 
T66 
>66 
query66 
=66 
_context66 &
.66& '
Set66' *
<66* +
T66+ ,
>66, -
(66- .
)66. /
;66/ 0
foreach77 
(77 
var77 
includeProperty77 $
in77% '
includeProperties77( 9
)779 :
{88 	
query99 
=99 
query99 
.99 
Include99 !
(99! "
includeProperty99" 1
)991 2
;992 3
}:: 	
return;; 
query;; 
;;; 
}<< 
}== ÷.
yC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Persistence\Repositories\InMemoryRepository.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %
Persistence% 0
.0 1
Repositories1 =
;= >
public 
class 
InMemoryRepository 
<  
T  !
>! "
:# $
IRepository% 0
<0 1
T1 2
>2 3
where4 9
T: ;
:< =

BaseEntity> H
{ 
	protected		 
readonly		 
List		 
<		 
T		 
>		 
	_entities		 (
=		) *
[		+ ,
]		, -
;		- .
public 

Task 
< 
T 
? 
> 
GetById 
( 
Guid  
id! #
,# $
CancellationToken% 6
cancellationToken7 H
)H I
{ 
var 
entity 
= 
	_entities 
. 
Find #
(# $
e$ %
=>& (
e) *
.* +
Id+ -
==. 0
id1 3
)3 4
;4 5
return 
Task 
. 

FromResult 
( 
entity %
)% &
;& '
} 
public 

Task 
< 
T 
? 
> 
GetByIdWithInclude &
(& '
Guid' +
id, .
,. /
CancellationToken0 A
cancellationTokenB S
,S T
paramsU [

Expression\ f
<f g
Funcg k
<k l
Tl m
,m n
objecto u
>u v
>v w
[w x
]x y
includeProperties	z ã
)
ã å
{ 
return 
GetById 
( 
id 
, 
cancellationToken ,
), -
;- .
} 
public 

Task 
< 
List 
< 
T 
> 
> 
GetAll 
(  
CancellationToken  1
cancellationToken2 C
)C D
{ 
return 
Task 
. 

FromResult 
( 
	_entities (
)( )
;) *
} 
public 

Task 
SaveChangesAsync  
(  !
CancellationToken! 2
cancellationToken3 D
)D E
{ 
return 
Task 
. 
CompletedTask !
;! "
} 
public   

Task   
<   
T   
>   
Add   
(   
T   
entity   
,    
CancellationToken  ! 2
cancellationToken  3 D
)  D E
{!! 
if"" 

("" 
entity"" 
."" 
Id"" 
=="" 
Guid"" 
."" 
Empty"" #
)""# $
{## 	
entity$$ 
.$$ 
Id$$ 
=$$ 
Guid$$ 
.$$ 
NewGuid$$ $
($$$ %
)$$% &
;$$& '
}%% 	
	_entities&& 
.&& 
Add&& 
(&& 
entity&& 
)&& 
;&& 
return'' 
Task'' 
.'' 

FromResult'' 
('' 
entity'' %
)''% &
;''& '
}(( 
public** 

Task** 
Delete** 
(** 
Guid** 
id** 
,** 
CancellationToken**  1
cancellationToken**2 C
)**C D
{++ 
var,, 
entity,, 
=,, 
	_entities,, 
.,, 
Find,, #
(,,# $
e,,$ %
=>,,& (
e,,) *
.,,* +
Id,,+ -
==,,. 0
id,,1 3
),,3 4
??,,5 7
throw,,8 =
new,,> A 
KeyNotFoundException,,B V
(,,V W
$",,W Y
$str,,Y h
{,,h i
id,,i k
},,k l
$str,,l v
",,v w
),,w x
;,,x y
	_entities-- 
.-- 
Remove-- 
(-- 
entity-- 
)--  
;--  !
return.. 
Task.. 
... 
CompletedTask.. !
;..! "
}// 
public11 

Task11 
<11 
T11 
>11 
Update11 
(11 
T11 
entity11 "
,11" #
CancellationToken11$ 5
cancellationToken116 G
)11G H
{22 
var33 
existingEntity33 
=33 
	_entities33 &
.33& '
Find33' +
(33+ ,
e33, -
=>33. 0
e331 2
.332 3
Id333 5
==336 8
entity339 ?
.33? @
Id33@ B
)33B C
??33D F
throw33G L
new33M P 
KeyNotFoundException33Q e
(33e f
$"33f h
$str33h w
{33w x
entity33x ~
.33~ 
Id	33 Å
}
33Å Ç
$str
33Ç å
"
33å ç
)
33ç é
;
33é è
	_entities44 
.44 
Remove44 
(44 
existingEntity44 '
)44' (
;44( )
	_entities55 
.55 
Add55 
(55 
entity55 
)55 
;55 
return66 
Task66 
.66 

FromResult66 
(66 
entity66 %
)66% &
;66& '
}77 
}88 ˆ
vC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Persistence\Repositories\GenreRepository.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %
Persistence% 0
.0 1
Repositories1 =
;= >
public 
class 
GenreRepository 
(  
ApplicationDbContext 1
context2 9
)9 :
:; <

Repository= G
<G H
GenreH M
>M N
(N O
contextO V
)V W
,W X
IGenreRepositoryY i
{		 
private

 
readonly

  
ApplicationDbContext

 )
_context

* 2
=

3 4
context

5 <
;

< =
public 

async 
Task 
< 
Genre 
? 
> 
	GetByName '
(' (
string( .
name/ 3
,3 4
CancellationToken5 F
cancellationTokenG X
)X Y
{ 
return 
await 
_context 
. 
Genres $
.$ %
FirstOrDefaultAsync% 8
(8 9
genre9 >
=>? A
genreB G
.G H
NameH L
==M O
nameP T
,T U
cancellationTokenV g
)g h
;h i
} 
public 

async 
Task 
< 
IEnumerable !
<! "
Genre" '
>' (
>( )$
GetByNameWhereIdNotEqual* B
(B C
stringC I
nameJ N
,N O
GuidP T
genreIdU \
,\ ]
CancellationToken^ o
cancellationToken	p Å
)
Å Ç
{ 
return 
await 
_context 
. 
Genres $
.$ %
Where% *
(* +
genre+ 0
=>1 3
genre4 9
.9 :
Name: >
==? A
nameB F
&&G I
genreJ O
.O P
IdP R
!=S U
genreIdV ]
)] ^
.^ _
ToListAsync_ j
(j k
cancellationTokenk |
)| }
;} ~
} 
} ç
vC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Persistence\Repositories\AlbumRepository.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %
Persistence% 0
.0 1
Repositories1 =
;= >
public 
class 
AlbumRepository 
(  
ApplicationDbContext 1
context2 9
)9 :
:; <

Repository= G
<G H
AlbumH M
>M N
(N O
contextO V
)V W
,W X
IAlbumRepositoryY i
{		 
private

 
readonly

  
ApplicationDbContext

 )
_context

* 2
=

3 4
context

5 <
;

< =
public 

async 
Task 
< 
Album 
? 
> 

GetByTitle (
(( )
string) /
title0 5
,5 6
CancellationToken7 H
cancellationTokenI Z
)Z [
{ 
return 
await 
_context 
. 
Albums $
.$ %
FirstOrDefaultAsync% 8
(8 9
album9 >
=>? A
albumB G
.G H
TitleH M
==N P
titleQ V
,V W
cancellationTokenX i
)i j
;j k
} 
public 

async 
Task 
< 
Album 
? 
>  
GetByOwnerIdAndTitle 2
(2 3
Guid3 7
ownerId8 ?
,? @
stringA G
titleH M
,M N
CancellationTokenO `
cancellationTokena r
)r s
{ 
return 
await 
_context 
. 
Albums $
.$ %
FirstOrDefaultAsync% 8
(8 9
album9 >
=>? A
albumB G
.G H
OwnerIdH O
==P R
ownerIdS Z
&&[ ]
album^ c
.c d
Titled i
==j l
titlem r
,r s
cancellationToken	t Ö
)
Ö Ü
;
Ü á
} 
public 

async 
Task 
< 
IEnumerable !
<! "
Album" '
>' (
>( )/
#GetByOwnerIdAndTitleWhereIdNotEqual* M
(M N
GuidN R
ownerIdS Z
,Z [
string\ b
titlec h
,h i
Guidj n
albumIdo v
,v w
CancellationToken	x â
cancellationToken
ä õ
)
õ ú
{ 
return 
await 
_context 
. 
Albums $
.$ %
Where% *
(* +
album+ 0
=>1 3
album4 9
.9 :
OwnerId: A
==B D
ownerIdE L
&&M O
albumP U
.U V
TitleV [
==\ ^
title_ d
&&e g
albumh m
.m n
Idn p
!=q s
albumIdt {
){ |
.| }
ToListAsync	} à
(
à â
cancellationToken
â ö
)
ö õ
;
õ ú
} 
} ≥
ÅC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Persistence\Migrations\20240418090034_InitialUpdate.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %
Persistence% 0
.0 1

Migrations1 ;
{ 
public		 

partial		 
class		 
InitialUpdate		 &
:		' (
	Migration		) 2
{

 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
DropForeignKey +
(+ ,
name 
: 
$str 7
,7 8
table 
: 
$str (
)( )
;) *
migrationBuilder 
. 
	DropIndex &
(& '
name 
: 
$str 1
,1 2
table 
: 
$str (
)( )
;) *
migrationBuilder 
. 

DropColumn '
(' (
name 
: 
$str 
, 
table 
: 
$str (
)( )
;) *
migrationBuilder 
. 
AddForeignKey *
(* +
name 
: 
$str 3
,3 4
table 
: 
$str (
,( )
column 
: 
$str 
, 
principalTable 
: 
$str  '
,' (
principalColumn 
:  
$str! %
,% &
onDelete   
:   
ReferentialAction   +
.  + ,
Cascade  , 3
)  3 4
;  4 5
}!! 	
	protected$$ 
override$$ 
void$$ 
Down$$  $
($$$ %
MigrationBuilder$$% 5
migrationBuilder$$6 F
)$$F G
{%% 	
migrationBuilder&& 
.&& 
DropForeignKey&& +
(&&+ ,
name'' 
:'' 
$str'' 3
,''3 4
table(( 
:(( 
$str(( (
)((( )
;(() *
migrationBuilder** 
.** 
	AddColumn** &
<**& '
Guid**' +
>**+ ,
(**, -
name++ 
:++ 
$str++ 
,++ 
table,, 
:,, 
$str,, (
,,,( )
type-- 
:-- 
$str-- (
,--( )
nullable.. 
:.. 
false.. 
,..  
defaultValue// 
:// 
Guid// "
.//" #
NewGuid//# *
(//* +
)//+ ,
)//, -
;//- .
migrationBuilder11 
.11 
CreateIndex11 (
(11( )
name22 
:22 
$str22 1
,221 2
table33 
:33 
$str33 (
,33( )
column44 
:44 
$str44  
,44  !
unique55 
:55 
true55 
)55 
;55 
migrationBuilder77 
.77 
AddForeignKey77 *
(77* +
name88 
:88 
$str88 7
,887 8
table99 
:99 
$str99 (
,99( )
column:: 
::: 
$str::  
,::  !
principalTable;; 
:;; 
$str;;  '
,;;' (
principalColumn<< 
:<<  
$str<<! %
,<<% &
onDelete== 
:== 
ReferentialAction== +
.==+ ,
Cascade==, 3
)==3 4
;==4 5
}>> 	
}?? 
}@@ ó∏
ÅC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Persistence\Migrations\20240417091931_InitialCreate.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %
Persistence% 0
.0 1

Migrations1 ;
{ 
public		 

partial		 
class		 
InitialCreate		 &
:		' (
	Migration		) 2
{

 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str 
, 
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
Guid& *
>* +
(+ ,
type, 0
:0 1
$str2 D
,D E
nullableF N
:N O
falseP U
)U V
,V W
Name 
= 
table  
.  !
Column! '
<' (
string( .
>. /
(/ 0
type0 4
:4 5
$str6 D
,D E
	maxLengthF O
:O P
$numQ S
,S T
nullableU ]
:] ^
false_ d
)d e
,e f
Description 
=  !
table" '
.' (
Column( .
<. /
string/ 5
>5 6
(6 7
type7 ;
:; <
$str= M
,M N
	maxLengthO X
:X Y
$numZ ^
,^ _
nullable` h
:h i
falsej o
)o p
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% 0
,0 1
x2 3
=>4 6
x7 8
.8 9
Id9 ;
); <
;< =
} 
) 
; 
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str 
, 
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
Guid& *
>* +
(+ ,
type, 0
:0 1
$str2 D
,D E
nullableF N
:N O
falseP U
)U V
,V W
Username   
=   
table   $
.  $ %
Column  % +
<  + ,
string  , 2
>  2 3
(  3 4
type  4 8
:  8 9
$str  : H
,  H I
	maxLength  J S
:  S T
$num  U W
,  W X
nullable  Y a
:  a b
false  c h
)  h i
,  i j
Email!! 
=!! 
table!! !
.!!! "
Column!!" (
<!!( )
string!!) /
>!!/ 0
(!!0 1
type!!1 5
:!!5 6
$str!!7 F
,!!F G
	maxLength!!H Q
:!!Q R
$num!!S V
,!!V W
nullable!!X `
:!!` a
false!!b g
)!!g h
,!!h i

ArtistName"" 
=""  
table""! &
.""& '
Column""' -
<""- .
string"". 4
>""4 5
(""5 6
type""6 :
:"": ;
$str""< K
,""K L
	maxLength""M V
:""V W
$num""X [
,""[ \
nullable""] e
:""e f
false""g l
)""l m
,""m n
DateOfBirth## 
=##  !
table##" '
.##' (
Column##( .
<##. /
DateOnly##/ 7
>##7 8
(##8 9
type##9 =
:##= >
$str##? E
,##E F
nullable##G O
:##O P
false##Q V
)##V W
,##W X
ProfilePictureUrl$$ %
=$$& '
table$$( -
.$$- .
Column$$. 4
<$$4 5
string$$5 ;
>$$; <
($$< =
type$$= A
:$$A B
$str$$C S
,$$S T
	maxLength$$U ^
:$$^ _
$num$$` d
,$$d e
nullable$$f n
:$$n o
true$$p t
)$$t u
}%% 
,%% 
constraints&& 
:&& 
table&& "
=>&&# %
{'' 
table(( 
.(( 

PrimaryKey(( $
((($ %
$str((% /
,((/ 0
x((1 2
=>((3 5
x((6 7
.((7 8
Id((8 :
)((: ;
;((; <
})) 
))) 
;)) 
migrationBuilder++ 
.++ 
CreateTable++ (
(++( )
name,, 
:,, 
$str,, 
,,, 
columns-- 
:-- 
table-- 
=>-- !
new--" %
{.. 
Id// 
=// 
table// 
.// 
Column// %
<//% &
Guid//& *
>//* +
(//+ ,
type//, 0
://0 1
$str//2 D
,//D E
nullable//F N
://N O
false//P U
)//U V
,//V W
Title00 
=00 
table00 !
.00! "
Column00" (
<00( )
string00) /
>00/ 0
(000 1
type001 5
:005 6
$str007 E
,00E F
	maxLength00G P
:00P Q
$num00R T
,00T U
nullable00V ^
:00^ _
false00` e
)00e f
,00f g
CoverImageUrl11 !
=11" #
table11$ )
.11) *
Column11* 0
<110 1
string111 7
>117 8
(118 9
type119 =
:11= >
$str11? O
,11O P
	maxLength11Q Z
:11Z [
$num11\ `
,11` a
nullable11b j
:11j k
true11l p
)11p q
,11q r
ReleaseDate22 
=22  !
table22" '
.22' (
Column22( .
<22. /
DateOnly22/ 7
>227 8
(228 9
type229 =
:22= >
$str22? E
,22E F
nullable22G O
:22O P
false22Q V
)22V W
,22W X
OwnerId33 
=33 
table33 #
.33# $
Column33$ *
<33* +
Guid33+ /
>33/ 0
(330 1
type331 5
:335 6
$str337 I
,33I J
nullable33K S
:33S T
false33U Z
)33Z [
}44 
,44 
constraints55 
:55 
table55 "
=>55# %
{66 
table77 
.77 

PrimaryKey77 $
(77$ %
$str77% 0
,770 1
x772 3
=>774 6
x777 8
.778 9
Id779 ;
)77; <
;77< =
table88 
.88 

ForeignKey88 $
(88$ %
name99 
:99 
$str99 7
,997 8
column:: 
::: 
x::  !
=>::" $
x::% &
.::& '
OwnerId::' .
,::. /
principalTable;; &
:;;& '
$str;;( /
,;;/ 0
principalColumn<< '
:<<' (
$str<<) -
,<<- .
onDelete==  
:==  !
ReferentialAction==" 3
.==3 4
Cascade==4 ;
)==; <
;==< =
}>> 
)>> 
;>> 
migrationBuilder@@ 
.@@ 
CreateTable@@ (
(@@( )
nameAA 
:AA 
$strAA '
,AA' (
columnsBB 
:BB 
tableBB 
=>BB !
newBB" %
{CC 
IdDD 
=DD 
tableDD 
.DD 
ColumnDD %
<DD% &
GuidDD& *
>DD* +
(DD+ ,
typeDD, 0
:DD0 1
$strDD2 D
,DDD E
nullableDDF N
:DDN O
falseDDP U
)DDU V
,DDV W
UserIdEE 
=EE 
tableEE "
.EE" #
ColumnEE# )
<EE) *
GuidEE* .
>EE. /
(EE/ 0
typeEE0 4
:EE4 5
$strEE6 H
,EEH I
nullableEEJ R
:EER S
falseEET Y
)EEY Z
,EEZ [
DarkModeFF 
=FF 
tableFF $
.FF$ %
ColumnFF% +
<FF+ ,
boolFF, 0
>FF0 1
(FF1 2
typeFF2 6
:FF6 7
$strFF8 =
,FF= >
nullableFF? G
:FFG H
falseFFI N
)FFN O
,FFO P
LanguageGG 
=GG 
tableGG $
.GG$ %
ColumnGG% +
<GG+ ,
stringGG, 2
>GG2 3
(GG3 4
typeGG4 8
:GG8 9
$strGG: I
,GGI J
nullableGGK S
:GGS T
falseGGU Z
)GGZ [
}HH 
,HH 
constraintsII 
:II 
tableII "
=>II# %
{JJ 
tableKK 
.KK 

PrimaryKeyKK $
(KK$ %
$strKK% 9
,KK9 :
xKK; <
=>KK= ?
xKK@ A
.KKA B
IdKKB D
)KKD E
;KKE F
tableLL 
.LL 

ForeignKeyLL $
(LL$ %
nameMM 
:MM 
$strMM ?
,MM? @
columnNN 
:NN 
xNN  !
=>NN" $
xNN% &
.NN& '
UserIdNN' -
,NN- .
principalTableOO &
:OO& '
$strOO( /
,OO/ 0
principalColumnPP '
:PP' (
$strPP) -
,PP- .
onDeleteQQ  
:QQ  !
ReferentialActionQQ" 3
.QQ3 4
CascadeQQ4 ;
)QQ; <
;QQ< =
}RR 
)RR 
;RR 
migrationBuilderTT 
.TT 
CreateTableTT (
(TT( )
nameUU 
:UU 
$strUU $
,UU$ %
columnsVV 
:VV 
tableVV 
=>VV !
newVV" %
{WW 
AlbumIdXX 
=XX 
tableXX #
.XX# $
ColumnXX$ *
<XX* +
GuidXX+ /
>XX/ 0
(XX0 1
typeXX1 5
:XX5 6
$strXX7 I
,XXI J
nullableXXK S
:XXS T
falseXXU Z
)XXZ [
,XX[ \
ArtistIdYY 
=YY 
tableYY $
.YY$ %
ColumnYY% +
<YY+ ,
GuidYY, 0
>YY0 1
(YY1 2
typeYY2 6
:YY6 7
$strYY8 J
,YYJ K
nullableYYL T
:YYT U
falseYYV [
)YY[ \
,YY\ ]
RoleZZ 
=ZZ 
tableZZ  
.ZZ  !
ColumnZZ! '
<ZZ' (
stringZZ( .
>ZZ. /
(ZZ/ 0
typeZZ0 4
:ZZ4 5
$strZZ6 D
,ZZD E
	maxLengthZZF O
:ZZO P
$numZZQ S
,ZZS T
nullableZZU ]
:ZZ] ^
falseZZ_ d
)ZZd e
}[[ 
,[[ 
constraints\\ 
:\\ 
table\\ "
=>\\# %
{]] 
table^^ 
.^^ 

PrimaryKey^^ $
(^^$ %
$str^^% 6
,^^6 7
x^^8 9
=>^^: <
new^^= @
{^^A B
x^^C D
.^^D E
AlbumId^^E L
,^^L M
x^^N O
.^^O P
ArtistId^^P X
,^^X Y
x^^Z [
.^^[ \
Role^^\ `
}^^a b
)^^b c
;^^c d
table__ 
.__ 

ForeignKey__ $
(__$ %
name`` 
:`` 
$str`` >
,``> ?
columnaa 
:aa 
xaa  !
=>aa" $
xaa% &
.aa& '
AlbumIdaa' .
,aa. /
principalTablebb &
:bb& '
$strbb( 0
,bb0 1
principalColumncc '
:cc' (
$strcc) -
,cc- .
onDeletedd  
:dd  !
ReferentialActiondd" 3
.dd3 4
Cascadedd4 ;
)dd; <
;dd< =
tableee 
.ee 

ForeignKeyee $
(ee$ %
nameff 
:ff 
$strff >
,ff> ?
columngg 
:gg 
xgg  !
=>gg" $
xgg% &
.gg& '
ArtistIdgg' /
,gg/ 0
principalTablehh &
:hh& '
$strhh( /
,hh/ 0
principalColumnii '
:ii' (
$strii) -
,ii- .
onDeletejj  
:jj  !
ReferentialActionjj" 3
.jj3 4
Restrictjj4 <
)jj< =
;jj= >
}kk 
)kk 
;kk 
migrationBuildermm 
.mm 
CreateTablemm (
(mm( )
namenn 
:nn 
$strnn 
,nn 
columnsoo 
:oo 
tableoo 
=>oo !
newoo" %
{pp 
Idqq 
=qq 
tableqq 
.qq 
Columnqq %
<qq% &
Guidqq& *
>qq* +
(qq+ ,
typeqq, 0
:qq0 1
$strqq2 D
,qqD E
nullableqqF N
:qqN O
falseqqP U
)qqU V
,qqV W
Titlerr 
=rr 
tablerr !
.rr! "
Columnrr" (
<rr( )
stringrr) /
>rr/ 0
(rr0 1
typerr1 5
:rr5 6
$strrr7 E
,rrE F
	maxLengthrrG P
:rrP Q
$numrrR T
,rrT U
nullablerrV ^
:rr^ _
falserr` e
)rre f
,rrf g
Durationss 
=ss 
tabless $
.ss$ %
Columnss% +
<ss+ ,
TimeSpanss, 4
>ss4 5
(ss5 6
typess6 :
:ss: ;
$strss< B
,ssB C
nullablessD L
:ssL M
falsessN S
)ssS T
,ssT U
Urltt 
=tt 
tablett 
.tt  
Columntt  &
<tt& '
stringtt' -
>tt- .
(tt. /
typett/ 3
:tt3 4
$strtt5 E
,ttE F
	maxLengthttG P
:ttP Q
$numttR V
,ttV W
nullablettX `
:tt` a
falsettb g
)ttg h
,tth i
OwnerIduu 
=uu 
tableuu #
.uu# $
Columnuu$ *
<uu* +
Guiduu+ /
>uu/ 0
(uu0 1
typeuu1 5
:uu5 6
$struu7 I
,uuI J
nullableuuK S
:uuS T
falseuuU Z
)uuZ [
,uu[ \
GenreIdvv 
=vv 
tablevv #
.vv# $
Columnvv$ *
<vv* +
Guidvv+ /
>vv/ 0
(vv0 1
typevv1 5
:vv5 6
$strvv7 I
,vvI J
nullablevvK S
:vvS T
truevvU Y
)vvY Z
,vvZ [
AlbumIdww 
=ww 
tableww #
.ww# $
Columnww$ *
<ww* +
Guidww+ /
>ww/ 0
(ww0 1
typeww1 5
:ww5 6
$strww7 I
,wwI J
nullablewwK S
:wwS T
truewwU Y
)wwY Z
}xx 
,xx 
constraintsyy 
:yy 
tableyy "
=>yy# %
{zz 
table{{ 
.{{ 

PrimaryKey{{ $
({{$ %
$str{{% /
,{{/ 0
x{{1 2
=>{{3 5
x{{6 7
.{{7 8
Id{{8 :
){{: ;
;{{; <
table|| 
.|| 

ForeignKey|| $
(||$ %
name}} 
:}} 
$str}} 7
,}}7 8
column~~ 
:~~ 
x~~  !
=>~~" $
x~~% &
.~~& '
AlbumId~~' .
,~~. /
principalTable &
:& '
$str( 0
,0 1
principalColumn
ÄÄ '
:
ÄÄ' (
$str
ÄÄ) -
,
ÄÄ- .
onDelete
ÅÅ  
:
ÅÅ  !
ReferentialAction
ÅÅ" 3
.
ÅÅ3 4
SetNull
ÅÅ4 ;
)
ÅÅ; <
;
ÅÅ< =
table
ÇÇ 
.
ÇÇ 

ForeignKey
ÇÇ $
(
ÇÇ$ %
name
ÉÉ 
:
ÉÉ 
$str
ÉÉ 7
,
ÉÉ7 8
column
ÑÑ 
:
ÑÑ 
x
ÑÑ  !
=>
ÑÑ" $
x
ÑÑ% &
.
ÑÑ& '
GenreId
ÑÑ' .
,
ÑÑ. /
principalTable
ÖÖ &
:
ÖÖ& '
$str
ÖÖ( 0
,
ÖÖ0 1
principalColumn
ÜÜ '
:
ÜÜ' (
$str
ÜÜ) -
,
ÜÜ- .
onDelete
áá  
:
áá  !
ReferentialAction
áá" 3
.
áá3 4
SetNull
áá4 ;
)
áá; <
;
áá< =
table
àà 
.
àà 

ForeignKey
àà $
(
àà$ %
name
ââ 
:
ââ 
$str
ââ 6
,
ââ6 7
column
ää 
:
ää 
x
ää  !
=>
ää" $
x
ää% &
.
ää& '
OwnerId
ää' .
,
ää. /
principalTable
ãã &
:
ãã& '
$str
ãã( /
,
ãã/ 0
principalColumn
åå '
:
åå' (
$str
åå) -
,
åå- .
onDelete
çç  
:
çç  !
ReferentialAction
çç" 3
.
çç3 4
Restrict
çç4 <
)
çç< =
;
çç= >
}
éé 
)
éé 
;
éé 
migrationBuilder
êê 
.
êê 
CreateIndex
êê (
(
êê( )
name
ëë 
:
ëë 
$str
ëë 0
,
ëë0 1
table
íí 
:
íí 
$str
íí %
,
íí% &
column
ìì 
:
ìì 
$str
ìì "
)
ìì" #
;
ìì# $
migrationBuilder
ïï 
.
ïï 
CreateIndex
ïï (
(
ïï( )
name
ññ 
:
ññ 
$str
ññ )
,
ññ) *
table
óó 
:
óó 
$str
óó 
,
óó  
column
òò 
:
òò 
$str
òò !
)
òò! "
;
òò" #
migrationBuilder
öö 
.
öö 
CreateIndex
öö (
(
öö( )
name
õõ 
:
õõ 
$str
õõ /
,
õõ/ 0
table
úú 
:
úú 
$str
úú 
,
úú  
columns
ùù 
:
ùù 
[
ùù 
$str
ùù !
,
ùù! "
$str
ùù# ,
]
ùù, -
,
ùù- .
unique
ûû 
:
ûû 
true
ûû 
)
ûû 
;
ûû 
migrationBuilder
†† 
.
†† 
CreateIndex
†† (
(
††( )
name
°° 
:
°° 
$str
°° &
,
°°& '
table
¢¢ 
:
¢¢ 
$str
¢¢ 
,
¢¢  
column
££ 
:
££ 
$str
££ 
,
££ 
unique
§§ 
:
§§ 
true
§§ 
)
§§ 
;
§§ 
migrationBuilder
¶¶ 
.
¶¶ 
CreateIndex
¶¶ (
(
¶¶( )
name
ßß 
:
ßß 
$str
ßß (
,
ßß( )
table
®® 
:
®® 
$str
®® 
,
®® 
column
©© 
:
©© 
$str
©© !
)
©©! "
;
©©" #
migrationBuilder
´´ 
.
´´ 
CreateIndex
´´ (
(
´´( )
name
¨¨ 
:
¨¨ 
$str
¨¨ (
,
¨¨( )
table
≠≠ 
:
≠≠ 
$str
≠≠ 
,
≠≠ 
column
ÆÆ 
:
ÆÆ 
$str
ÆÆ !
)
ÆÆ! "
;
ÆÆ" #
migrationBuilder
∞∞ 
.
∞∞ 
CreateIndex
∞∞ (
(
∞∞( )
name
±± 
:
±± 
$str
±± (
,
±±( )
table
≤≤ 
:
≤≤ 
$str
≤≤ 
,
≤≤ 
column
≥≥ 
:
≥≥ 
$str
≥≥ !
)
≥≥! "
;
≥≥" #
migrationBuilder
µµ 
.
µµ 
CreateIndex
µµ (
(
µµ( )
name
∂∂ 
:
∂∂ 
$str
∂∂ .
,
∂∂. /
table
∑∑ 
:
∑∑ 
$str
∑∑ 
,
∑∑ 
columns
∏∏ 
:
∏∏ 
[
∏∏ 
$str
∏∏ !
,
∏∏! "
$str
∏∏# ,
]
∏∏, -
,
∏∏- .
unique
ππ 
:
ππ 
true
ππ 
)
ππ 
;
ππ 
migrationBuilder
ªª 
.
ªª 
CreateIndex
ªª (
(
ªª( )
name
ºº 
:
ºº 
$str
ºº 1
,
ºº1 2
table
ΩΩ 
:
ΩΩ 
$str
ΩΩ (
,
ΩΩ( )
column
ææ 
:
ææ 
$str
ææ  
,
ææ  !
unique
øø 
:
øø 
true
øø 
)
øø 
;
øø 
migrationBuilder
¡¡ 
.
¡¡ 
CreateIndex
¡¡ (
(
¡¡( )
name
¬¬ 
:
¬¬ 
$str
¬¬ &
,
¬¬& '
table
√√ 
:
√√ 
$str
√√ 
,
√√ 
column
ƒƒ 
:
ƒƒ 
$str
ƒƒ 
,
ƒƒ  
unique
≈≈ 
:
≈≈ 
true
≈≈ 
)
≈≈ 
;
≈≈ 
migrationBuilder
«« 
.
«« 
CreateIndex
«« (
(
««( )
name
»» 
:
»» 
$str
»» )
,
»») *
table
…… 
:
…… 
$str
…… 
,
…… 
column
   
:
   
$str
   "
,
  " #
unique
ÀÀ 
:
ÀÀ 
true
ÀÀ 
)
ÀÀ 
;
ÀÀ 
}
ÃÃ 	
	protected
œœ 
override
œœ 
void
œœ 
Down
œœ  $
(
œœ$ %
MigrationBuilder
œœ% 5
migrationBuilder
œœ6 F
)
œœF G
{
–– 	
migrationBuilder
—— 
.
—— 
	DropTable
—— &
(
——& '
name
““ 
:
““ 
$str
““ $
)
““$ %
;
““% &
migrationBuilder
‘‘ 
.
‘‘ 
	DropTable
‘‘ &
(
‘‘& '
name
’’ 
:
’’ 
$str
’’ 
)
’’ 
;
’’ 
migrationBuilder
◊◊ 
.
◊◊ 
	DropTable
◊◊ &
(
◊◊& '
name
ÿÿ 
:
ÿÿ 
$str
ÿÿ '
)
ÿÿ' (
;
ÿÿ( )
migrationBuilder
⁄⁄ 
.
⁄⁄ 
	DropTable
⁄⁄ &
(
⁄⁄& '
name
€€ 
:
€€ 
$str
€€ 
)
€€ 
;
€€  
migrationBuilder
›› 
.
›› 
	DropTable
›› &
(
››& '
name
ﬁﬁ 
:
ﬁﬁ 
$str
ﬁﬁ 
)
ﬁﬁ 
;
ﬁﬁ  
migrationBuilder
‡‡ 
.
‡‡ 
	DropTable
‡‡ &
(
‡‡& '
name
·· 
:
·· 
$str
·· 
)
·· 
;
·· 
}
‚‚ 	
}
„„ 
}‰‰ ¯
wC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Persistence\Contexts\ApplicationDbContext.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %
Persistence% 0
.0 1
Contexts1 9
;9 :
public 
class  
ApplicationDbContext !
(! "
DbContextOptions" 2
<2 3 
ApplicationDbContext3 G
>G H
optionsI P
)P Q
:R S
	DbContextT ]
(] ^
options^ e
)e f
{ 
public		 

DbSet		 
<		 
Genre		 
>		 
Genres		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
public

 

DbSet

 
<

 
User

 
>

 
Users

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
public 

DbSet 
< 
Album 
> 
Albums 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 

DbSet 
< 
AlbumArtist 
> 
AlbumArtists *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 

DbSet 
< 
Song 
> 
Songs 
{ 
get "
;" #
set$ '
;' (
}) *
public 

DbSet 
< 
UserPreference 
>  
UserPreferences! 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
	protected 
override 
void 
OnModelCreating +
(+ ,
ModelBuilder, 8
modelBuilder9 E
)E F
{ 
base 
. 
OnModelCreating 
( 
modelBuilder )
)) *
;* +
var 
assembly 
= 
typeof 
( !
UserPreferencesConfig 3
)3 4
.4 5
Assembly5 =
;= >
modelBuilder 
. +
ApplyConfigurationsFromAssembly 4
(4 5
assembly5 =
)= >
;> ?
} 
} ’ 
lC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Mapping\Profiles\MapsterConfig.cs
	namespace		 	
Streamphony		
 
.		 
Infrastructure		 $
.		$ %
Mapping		% ,
.		, -
Profiles		- 5
;		5 6
public 
static 
class 
MapsterConfig !
{ 
public 

static 
TypeAdapterConfig #
GlobalConfig$ 0
{1 2
get3 6
;6 7
}8 9
static 

MapsterConfig 
( 
) 
{ 
GlobalConfig 
= 
new 
TypeAdapterConfig ,
(, -
)- .
;. /
GlobalConfig 
. 
	NewConfig 
< 
User #
,# $
UserCreationDto% 4
>4 5
(5 6
)6 7
.7 8
TwoWays8 ?
(? @
)@ A
;A B
GlobalConfig 
. 
	NewConfig 
< 
User #
,# $
UserDto% ,
>, -
(- .
). /
./ 0
TwoWays0 7
(7 8
)8 9
;9 :
GlobalConfig 
. 
	NewConfig 
< 
User #
,# $
UserDetailsDto% 3
>3 4
(4 5
)5 6
.6 7
PreserveReference7 H
(H I
trueI M
)M N
;N O
GlobalConfig 
. 
	NewConfig 
< 
Song #
,# $
SongCreationDto% 4
>4 5
(5 6
)6 7
.7 8
TwoWays8 ?
(? @
)@ A
;A B
GlobalConfig 
. 
	NewConfig 
< 
Song #
,# $
SongDto% ,
>, -
(- .
). /
./ 0
TwoWays0 7
(7 8
)8 9
;9 :
GlobalConfig 
. 
	NewConfig 
< 
Genre $
,$ %
GenreCreationDto& 6
>6 7
(7 8
)8 9
.9 :
TwoWays: A
(A B
)B C
;C D
GlobalConfig 
. 
	NewConfig 
< 
Genre $
,$ %
GenreDto& .
>. /
(/ 0
)0 1
.1 2
TwoWays2 9
(9 :
): ;
;; <
GlobalConfig 
. 
	NewConfig 
< 
Genre $
,$ %
GenreDetailsDto& 5
>5 6
(6 7
)7 8
.8 9
PreserveReference9 J
(J K
trueK O
)O P
;P Q
GlobalConfig 
. 
	NewConfig 
< 
UserPreference -
,- .
UserPreferenceDto/ @
>@ A
(A B
)B C
.C D
TwoWaysD K
(K L
)L M
;M N
GlobalConfig   
.   
	NewConfig   
<   
Album   $
,  $ %
AlbumCreationDto  & 6
>  6 7
(  7 8
)  8 9
.  9 :
TwoWays  : A
(  A B
)  B C
;  C D
GlobalConfig!! 
.!! 
	NewConfig!! 
<!! 
Album!! $
,!!$ %
AlbumDto!!& .
>!!. /
(!!/ 0
)!!0 1
.!!1 2
TwoWays!!2 9
(!!9 :
)!!: ;
;!!; <
GlobalConfig"" 
."" 
	NewConfig"" 
<"" 
Album"" $
,""$ %
AlbumDetailsDto""& 5
>""5 6
(""6 7
)""7 8
.""8 9
PreserveReference""9 J
(""J K
true""K O
)""O P
;""P Q
GlobalConfig$$ 
.$$ 
Compile$$ 
($$ 
)$$ 
;$$ 
}%% 
}&& Ú
eC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Mapping\MapsterProvider.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %
Mapping% ,
;, -
public 
class 
MapsterProvider 
: 
IMappingProvider /
{ 
private		 
readonly		 
TypeAdapterConfig		 &
_config		' .
;		. /
public 

MapsterProvider 
( 
) 
{ 
_config 
= 
MapsterConfig 
.  
GlobalConfig  ,
;, -
} 
public 

TDestination 
Map 
< 
TDestination (
>( )
() *
object* 0
?0 1
source2 8
)8 9
{ 
return 
source 
. 
Adapt 
< 
TDestination (
>( )
() *
_config* 1
)1 2
;2 3
} 
public 

TDestination 
Map 
< 
TSource #
,# $
TDestination% 1
>1 2
(2 3
TSource3 :
source; A
,A B
TDestinationC O
destinationP [
)[ \
{ 
return 
source 
. 
Adapt 
( 
destination '
,' (
_config) 0
)0 1
;1 2
} 
} Ÿ#
eC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Logging\SerilogProvider.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %
Logging% ,
;, -
public 
class 
SerilogProvider 
: 
ILoggingProvider /
{ 
public 

void 
LogInformation 
( 
string %
message& -
)- .
{		 
Log

 
.

 
Information

 
(

 
message

 
)

  
;

  !
} 
public 

void 
LogInformation 
( 
string %
messageTemplate& 5
,5 6
params7 =
object> D
?D E
[E F
]F G
?G H
propertyValuesI W
)W X
{ 
Log 
. 
Information 
( 
messageTemplate '
,' (
propertyValues) 7
)7 8
;8 9
} 
public 

void 
LogInformation 
( 
	Exception (
?( )
	exception* 3
,3 4
string5 ;
messageTemplate< K
,K L
paramsM S
objectT Z
?Z [
[[ \
]\ ]
?] ^
propertyValues_ m
)m n
{ 
Log 
. 
Information 
( 
	exception !
,! "
messageTemplate# 2
,2 3
propertyValues4 B
)B C
;C D
} 
public 

void 

LogWarning 
( 
string !
message" )
)) *
{ 
Log 
. 
Warning 
( 
message 
) 
; 
} 
public 

void 

LogWarning 
( 
string !
messageTemplate" 1
,1 2
params3 9
object: @
?@ A
[A B
]B C
?C D
propertyValuesE S
)S T
{ 
Log 
. 
Warning 
( 
messageTemplate #
,# $
propertyValues% 3
)3 4
;4 5
} 
public!! 

void!! 

LogWarning!! 
(!! 
	Exception!! $
?!!$ %
	exception!!& /
,!!/ 0
string!!1 7
messageTemplate!!8 G
,!!G H
params!!I O
object!!P V
?!!V W
[!!W X
]!!X Y
?!!Y Z
propertyValues!![ i
)!!i j
{"" 
Log## 
.## 
Warning## 
(## 
	exception## 
,## 
messageTemplate## .
,##. /
propertyValues##0 >
)##> ?
;##? @
}$$ 
public&& 

void&& 
LogError&& 
(&& 
string&& 
message&&  '
,&&' (
	Exception&&) 2
ex&&3 5
)&&5 6
{'' 
Log(( 
.(( 
Error(( 
((( 
ex(( 
,(( 
message(( 
)(( 
;(( 
})) 
public++ 

void++ 
LogError++ 
(++ 
string++ 
messageTemplate++  /
,++/ 0
params++1 7
object++8 >
?++> ?
[++? @
]++@ A
?++A B
propertyValues++C Q
)++Q R
{,, 
Log-- 
.-- 
Error-- 
(-- 
messageTemplate-- !
,--! "
propertyValues--# 1
)--1 2
;--2 3
}.. 
public00 

void00 
LogError00 
(00 
	Exception00 "
?00" #
	exception00$ -
,00- .
string00/ 5
messageTemplate006 E
,00E F
params00G M
object00N T
?00T U
[00U V
]00V W
?00W X
propertyValues00Y g
)00g h
{11 
Log22 
.22 
Error22 
(22 
	exception22 
,22 
messageTemplate22 ,
,22, -
propertyValues22. <
)22< =
;22= >
}33 
}44 ı
lC:\Personal\amdaris\project\streamphony-asp-net\Streamphony.Infrastructure\Extensions\ValidatorExtensions.cs
	namespace 	
Streamphony
 
. 
Infrastructure $
.$ %

Extensions% /
;/ 0
public 
static 
class 
ValidatorExtensions '
{ 
public 

static 
IRuleBuilderOptions %
<% &
T& '
,' (
DateOnly) 1
>1 2
DateNotInFuture3 B
<B C
TC D
>D E
(E F
thisF J
IRuleBuilderK W
<W X
TX Y
,Y Z
DateOnly[ c
>c d
ruleBuildere p
)p q
{ 
return		 
ruleBuilder		 
.		 
Must		 
(		  
date		  $
=>		% '
date		( ,
<=		- /
DateOnly		0 8
.		8 9
FromDateTime		9 E
(		E F
DateTime		F N
.		N O
UtcNow		O U
)		U V
)		V W
.		W X
WithMessage		X c
(		c d
$str			d å
)
		å ç
;
		ç é
}

 
public 

static 
IRuleBuilderOptions %
<% &
T& '
,' (
string) /
?/ 0
>0 1
ValidUrl2 :
<: ;
T; <
>< =
(= >
this> B
IRuleBuilderC O
<O P
TP Q
,Q R
stringS Y
?Y Z
>Z [
ruleBuilder\ g
)g h
{ 
return 
ruleBuilder 
. 
Must 
( 
url 
=> 
string 
.  
IsNullOrEmpty  -
(- .
url. 1
)1 2
||3 5
Uri6 9
.9 :
	TryCreate: C
(C D
urlD G
,G H
UriKindI P
.P Q
AbsoluteQ Y
,Y Z
out[ ^
__ `
)` a
)a b
. 
WithMessage 
( 
$str <
)< =
;= >
} 
} 