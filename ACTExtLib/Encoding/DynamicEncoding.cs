using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using ACT.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ACT.Core.Extensions
{
   public static class EncodingExtensions
   {
      public static string GenerateEncodedString(this string x, int EncodingBitLength)
      {
         DynamicEncoding _H = new DynamicEncoding();

         if (_H.Loaded == false) { return null; }

         byte[] Data = System.Text.UTF32Encoding.UTF8.GetBytes(x);
         string _Alpha = _H.DAlph[EncodingBitLength];
         //0010011100010000

         return null;

         //TODO ASAP
      }
   }
   
   internal class DynamicEncoding
   {
      private const string DefaultAlphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzªµºÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿĀāĂăĄąĆćĈĉĊċČčĎďĐđĒēĔĕĖėĘęĚěĜĝĞğĠġĢģĤĥĦħĨĩĪīĬĭĮįİıĲĳĴĵĶķĸĹĺĻļĽľĿŀŁłŃńŅņŇňŉŊŋŌōŎŏŐőŒœŔŕŖŗŘřŚśŜŝŞşŠšŢţŤťŦŧŨũŪūŬŭŮůŰűŲųŴŵŶŷŸŹźŻżŽžſƀƁƂƃƄƅƆƇƈƉƊƋƌƍƎƏƐƑƒƓƔƕƖƗƘƙƚƛƜƝƞƟƠơƢƣƤƥƦƧƨƩƪƫƬƭƮƯưƱƲƳƴƵƶƷƸƹƺƻƼƽƾƿǀǁǂǃǄǅǆǇǈǉǊǋǌǍǎǏǐǑǒǓǔǕǖǗǘǙǚǛǜǝǞǟǠǡǢǣǤǥǦǧǨǩǪǫǬǭǮǯǰǱǲǳǴǵǶǷǸǹǺǻǼǽǾǿȀȁȂȃȄȅȆȇȈȉȊȋȌȍȎȏȐȑȒȓȔȕȖȗȘșȚțȜȝȞȟȠȡȢȣȤȥȦȧȨȩȪȫȬȭȮȯȰȱȲȳȴȵȶȸȹȺȻȼȽȾȿɀɁɂɃɄɅɆɇɈɉɊɋɌɍɎɏɐɑɒɓɔɕɖɗɘəɚɛɜɝɞɟɠɡɢɣɤɥɦɧɨɩɪɫɬɭɮɯɰɱɲɳɴɵɶɷɸɹɺɻɼɽɾɿʀʁʂʃʄʅʆʇʈʉʊʋʌʍʎʏʐʑʒʓʔʕʖʗʘʙʚʛʜʝʞʟʠʡʢʣʤʥʦʧʨʩʪʫʬʭʮʯʰʱʲʳʴʵʶʷʸʹʺʻʼʽʾʿˀˁˆˇˈˉˊˋˌˍˎˏːˑˠˡˢˣˤˬˮʹͺͻͼͽΆΈΉΊΌΎΏΐΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩΪΫάέήίΰαβγδεζηθικλμνξοπρςστυφχψωϊϋόύώϐϑϒϓϔϕϖϗϘϙϚϛϜϝϞϟϠϡϢϣϤϥϦϧϨϩϪϫϬϭϮϯϰϱϲϳϴϵϷϸϹϺϻϼϽϾϿЀЁЂЃЄЅІЇЈЉЊЋЌЍЎЏАБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдежзийклмнопрстуфхцчшщъыьэюяѐёђѓєѕіїјљњћќѝўџѠѡѢѣѤѥѦѧѨѩѪѫѬѭѮѯѰѱѲѳѴѵѶѷѸѹѺѻѼѽѾѿҀҁҊҋҌҍҎҏҐґҒғҔҕҖҗҘҙҚқҜҝҞҟҠҡҢңҤҥҦҧҨҩҪҫҬҭҮүҰұҲҳҴҵҶҷҸҹҺһҼҽҾҿӀӁӂӃӄӅӆӇӈӉӊӋӌӍӎӏӐӑӒӓӔӕӖӗӘәӚӛӜӝӞӟӠӡӢӣӤӥӦӧӨөӪӫӬӭӮӯӰӱӲӳӴӵӶӷӸӹӺӻӼӽӾӿԀԁԂԃԄԅԆԇԈԉԊԋԌԍԎԏԐԑԒԓԚԛԜԝԱԲԳԴԵԶԷԸԹԺԻԼԽԾԿՀՁՂՃՄՅՆՇՈՉՊՋՌՍՎՏՐՑՒՓՔՙաբգդեզէըթժիլխծկհձղճմյնշոչպջռսվտրցւփքօֆևאבגדהוזחטיךכלםמןנסעףפץצקרשתװױײءآأؤإئابةتثجحخدذرزسشصضطظعغػؼؽؾؿـفقكلمنهوىي٠١٢٣٤٥٦٧٨٩ٮٯٱٲٳٴٵٶٷٸٹٺٻټٽپٿڀځڂڃڄڅچڇڈډڊڋڌڍڎڏڐڑڒړڔڕږڗژڙښڛڜڝڞڟڠڡڢڣڤڥڦڧڨکڪګڬڭڮگڰڱڲڳڴڵڶڷڸڹںڻڼڽھڿۀہۂۃۄۅۆۇۈۉۊۋیۍێۏېۑےۓەۥۦۮۯ۰۱۲۳۴۵۶۷۸۹ۺۻۼۿܐܒܓܔܕܖܗܘܙܚܛܜܝܞܟܠܡܢܣܤܥܦܧܨܩܪܫܬܭܮܯݍݎݏݐݑݒݓݔݕݖݗݘݙݚݛݜݝݞݟݠݡݢݣݤݥݦݧݨݩݪݫݬݭݮݯݰݱݲݳݴݵݶݷݸݹݺݻݼݽݾݿހށނރބޅކއވމފދތލގޏސޑޒޓޔޕޖޗޘޙޚޛޜޝޞޟޠޡޢޣޤޥޱ߀߁߂߃߄߅߆߇߈߉ߊߋߌߍߎߏߐߑߒߓߔߕߖߗߘߙߚߛߜߝߞߟߠߡߢߣߤߥߦߧߨߩߪߴߵߺऄअआइईउऊऋऌऍऎएऐऑऒओऔकखगघङचछजझञटठडढणतथदधनऩपफबभमयरऱलळऴवशषसहऽॐक़ख़ग़ज़ड़ढ़फ़य़ॠॡ०१२३४५६७८९ॱॲॻॼॽॾॿঅআইঈউঊঋঌএঐওঔকখগঘঙচছজঝঞটঠডঢণতথদধনপফবভমযরলশষসহঽৎড়ঢ়য়ৠৡ০১২৩৪৫৬৭৮৯ৰৱਅਆਇਈਉਊਏਐਓਔਕਖਗਘਙਚਛਜਝਞਟਠਡਢਣਤਥਦਧਨਪਫਬਭਮਯਰਲਲ਼ਵਸ਼ਸਹਖ਼ਗ਼ਜ਼ੜਫ਼੦੧੨੩੪੫੬੭੮੯ੲੳੴઅઆઇઈઉઊઋઌઍએઐઑઓઔકખગઘઙચછજઝઞટઠડઢણતથદધનપફબભમયરલળવશષસહઽૐૠૡ૦૧૨૩૪૫૬૭૮૯ଅଆଇଈଉଊଋଌଏଐଓଔକଖଗଘଙଚଛଜଝଞଟଠଡଢଣତଥଦଧନପଫବଭମଯରଲଳଵଶଷସହଽଡ଼ଢ଼ୟୠୡ୦୧୨୩୪୫୬୭୮୯ୱஃஅஆஇஈஉஊஎஏஐஒஓஔகஙசஜஞடணதநனபமயரறலளழவஶஷஸஹௐ௦௧௨௩௪௫௬௭௮௯అఆఇఈఉఊఋఌఎఏఐఒఓఔకఖగఘఙచఛజఝఞటఠడఢణతథదధనపఫబభమయరఱలళవశషసహఽౘౙౠౡ౦౧౨౩౪౫౬౭౮౯ಅಆಇಈಉಊಋಌಎಏಐಒಓಔಕಖಗಘಙಚಛಜಝಞಟಠಡಢಣತಥದಧನಪಫಬಭಮಯರಱಲಳವಶಷಸಹಽೞೠೡ೦೧೨೩೪೫೬೭೮೯അആഇഈഉഊഋഌഎഏഐഒഓഔകഖഗഘങചഛജഝഞടഠഡഢണതഥദധനപഫബഭമയരറലളഴവശഷസഹഽൠൡ൦൧൨൩൪൫൬൭൮൯ൺൻർൽൾൿඅආඇඈඉඊඋඌඍඎඏඐඑඒඓඔඕඖකඛගඝඞඟචඡජඣඤඥඦටඨඩඪණඬතථදධනඳපඵබභමඹයරලවශෂසහළෆกขฃคฅฆงจฉชซฌญฎฏฐฑฒณดตถทธนบปผฝพฟภมยรฤลฦวศษสหฬอฮฯะาำเแโใไๅๆ๐๑๒๓๔๕๖๗๘๙ກຂຄງຈຊຍດຕຖທນບປຜຝພຟມຢຣລວສຫອຮຯະາຳຽເແໂໃໄໆ໐໑໒໓໔໕໖໗໘໙ໜໝༀ༠༡༢༣༤༥༦༧༨༩ཀཁགགྷངཅཆཇཉཊཋཌཌྷཎཏཐདདྷནཔཕབབྷམཙཚཛཛྷཝཞཟའཡརལཤཥསཧཨཀྵཪྈྉྊྋჺჼᄀᄁᄂᄃᄄᄅᄆᄇᄈᄉᄊᄋᄌᄍᄎᄏᄐᄑᄒᄓᄔᄕᄖᄗᄘᄙᄚᄛᄜᄝᄞᄟᄠᄡᄢᄣᄤᄥᄦᄧᄨᄩᄪᄫᄬᄭᄮᄯᄰᄱᄲᄳᄴᄵᄶᄷᄸᄹᄺᄻᄼᄽᄾᄿᅀᅁᅂᅃᅄᅅᅆᅇᅈᅉᅊᅋᅌᅍᅎᅏᅐᅑᅒᅓᅔᅕᅖᅗᅘᅙᅟᅠᅡᅢᅣᅤᅥᅦᅧᅨᅩᅪᅫᅬᅭᅮᅯᅰᅱᅲᅳᅴᅵᅶᅷᅸᅹᅺᅻᅼᅽᅾᅿᆀᆁᆂᆃᆄᆅᆆᆇᆈᆉᆊᆋᆌᆍᆎᆏᆐᆑᆒᆓᆔᆕᆖᆗᆘᆙᆚᆛᆜᆝᆞᆟᆠᆡᆢᆨᆩᆪᆫᆬᆭᆮᆯᆰᆱᆲᆳᆴᆵᆶᆷᆸᆹᆺᆻᆼᆽᆾᆿᇀᇁᇂᇃᇄᇅᇆᇇᇈᇉᇊᇋᇌᇍᇎᇏᇐᇑᇒᇓᇔᇕᇖᇗᇘᇙᇚᇛᇜᇝᇞᇟᇠᇡᇢᇣᇤᇥᇦᇧᇨᇩᇪᇫᇬᇭᇮᇯᇰᇱᇲᇳᇴᇵᇶᇷᇸᇹሀሁሂሃሄህሆሇለሉሊላሌልሎሏሐሑሒሓሔሕሖሗመሙሚማሜምሞሟሠሡሢሣሤሥሦሧረሩሪራሬርሮሯሰሱሲሳሴስሶሷሸሹሺሻሼሽሾሿቀቁቂቃቄቅቆቇቈቊቋቌቍቐቑቒቓቔቕቖቘቚቛቜቝበቡቢባቤብቦቧቨቩቪቫቬቭቮቯተቱቲታቴትቶቷቸቹቺቻቼችቾቿኀኁኂኃኄኅኆኇኈኊኋኌኍነኑኒናኔንኖኗኘኙኚኛኜኝኞኟአኡኢኣኤእኦኧከኩኪካኬክኮኯኰኲኳኴኵኸኹኺኻኼኽኾዀዂዃዄዅወዉዊዋዌውዎዏዐዑዒዓዔዕዖዘዙዚዛዜዝዞዟዠዡዢዣዤዥዦዧየዩዪያዬይዮዯደዱዲዳዴድዶዷዸዹዺዻዼዽዾዿጀጁጂጃጄጅጆጇገጉጊጋጌግጎጏጐጒጓጔጕጘጙጚጛጜጝጞጟጠጡጢጣጤጥጦጧጨጩጪጫጬጭጮጯጰጱጲጳጴጵጶጷጸጹጺጻጼጽጾጿፀፁፂፃፄፅፆፇፈፉፊፋፌፍፎፏፐፑፒፓፔፕፖፗፘፙፚᎀᎁᎂᎃᎄᎅᎆᎇᎈᎉᎊᎋᎌᎍᎎᎏᎠᎡᎢᎣᎤᎥᎦᎧᎨᎩᎪᎫᎬᎭᎮᎯᎰᎱᎲᎳᎴᎵᎶᎷᎸᎹᎺᎻᎼᎽᎾᎿᏀᏁᏂᏃᏄᏅᏆᏇᏈᏉᏊᏋᏌᏍᏎᏏᏐᏑᏒᏓᏔᏕᏖᏗᏘᏙᏚᏛᏜᏝᏞᏟᏠᏡᏢᏣᏤᏥᏦᏧᏨᏩᏪᏫᏬᏭᏮᏯᏰᏱᏲᏳᏴᐁᐂᐃᐄᐅᐆᐇᐈᐉᐊᐋᐌᐍᐎᐏᐐᐑᐒᐓᐔᐕᐖᐗᐘᐙᐚᐛᐜᐝᐞᐟᐠᐡᐢᐣᐤᐥᐦᐧᐨᐩᐪᐫᐬᐭᐮᐯᐰᐱᐲᐳᐴᐵᐶᐷᐸᐹᐺᐻᐼᐽᐾᐿᑀᑁᑂᑃᑄᑅᑆᑇᑈᑉᑊᑋᑌᑍᑎᑏᑐᑑᑒᑓᑔᑕᑖᑗᑘᑙᑚᑛᑜᑝᑞᑟᑠᑡᑢᑣᑤᑥᑦᑧᑨᑩᑪᑫᑬᑭᑮᑯᑰᑱᑲᑳᑴᑵᑶᑷᑸᑹᑺᑻᑼᑽᑾᑿᒀᒁᒂᒃᒄᒅᒆᒇᒈᒉᒊᒋᒌᒍᒎᒏᒐᒑᒒᒓᒔᒕᒖᒗᒘᒙᒚᒛᒜᒝᒞᒟᒠᒡᒢᒣᒤᒥᒦᒧᒨᒩᒪᒫᒬᒭᒮᒯᒰᒱᒲᒳᒴᒵᒶᒷᒸᒹᒺᒻᒼᒽᒾᒿᓀᓁᓂᓃᓄᓅᓆᓇᓈᓉᓊᓋᓌᓍᓎᓏᓐᓑᓒᓓᓔᓕᓖᓗᓘᓙᓚᓛᓜᓝᓞᓟᓠᓡᓢᓣᓤᓥᓦᓧᓨᓩᓪᓫᓬᓭᓮᓯᓰᓱᓲᓳᓴᓵᓶᓷᓸᓹᓺᓻᓼᓽᓾᓿᔀᔁᔂᔃᔄᔅᔆᔇᔈᔉᔊᔋᔌᔍᔎᔏᔐᔑᔒᔓᔔᔕᔖᔗᔘᔙᔚᔛᔜᔝᔞᔟᔠᔡᔢᔣᔤᔥᔦᔧᔨᔩᔪᔫᔬᔭᔮᔯᔰᔱᔲᔳᔴᔵᔶᔷᔸᔹᔺᔻᔼᔽᔾᔿᕀᕁᕂᕃᕄᕅᕆᕇᕈᕉᕊᕋᕌᕍᕎᕏᕐᕑᕒᕓᕔᕕᕖᕗᕘᕙᕚᕛᕜᕝᕞᕟᕠᕡᕢᕣᕤᕥᕦᕧᕨᕩᕪᕫᕬᕭᕮᕯᕰᕱᕲᕳᕴᕵᕶᕷᕸᕹᕺᕻᕼᕽᕾᕿᖀᖁᖂᖃᖄᖅᖆᖇᖈᖉᖊᖋᖌᖍᖎᖏᖐᖑᖒᖓᖔᖕᖖᖗᖘᖙᖚᖛᖜᖝᖞᖟᖠᖡᖢᖣᖤᖥᖦᖧᖨᖩᖪᖫᖬᖭᖮᖯᖰᖱᖲᖳᖴᖵᖶᖷᖸᖹᖺᖻᖼᖽᖾᖿᗀᗁᗂᗃᗄᗅᗆᗇᗈᗉᗊᗋᗌᗍᗎᗏᗐᗑᗒᗓᗔᗕᗖᗗᗘᗙᗚᗛᗜᗝᗞᗟᗠᗡᗢᗣᗤᗥᗦᗧᗨᗩᗪᗫᗬᗭᗮᗯᗰᗱᗲᗳᗴᗵᗶᗷᗸᗹᗺᗻᗼᗽᗾᗿᘀᘁᘂᘃᘄᘅᘆᘇᘈᘉᘊᘋᘌᘍᘎᘏᘐᘑᘒᘓᘔᘕᘖᘗᘘᘙᘚᘛᘜᘝᘞᘟᘠᘡᘢᘣᘤᘥᘦᘧᘨᘩᘪᘫᘬᘭᘮᘯᘰᘱᘲᘳᘴᘵᘶᘷᘸᘹᘺᘻᘼᘽᘾᘿᙀᙁᙂᙃᙄᙅᙆᙇᙈᙉᙊᙋᙌᙍᙎᙏᙐᙑᙒᙓᙔᙕᙖᙗᙘᙙᙚᙛᙜᙝᙞᙟᙠᙡᙢᙣᙤᙥᙦᙧᙨᙩᙪᙫᙬᙯᙰᙱᙲᙳᙴᙵᙶᚁᚂᚃᚄᚅᚆᚇᚈᚉᚊᚋᚌᚍᚎᚏᚐᚑᚒᚓᚔᚕᚖᚗᚘᚙᚚᚠᚡᚢᚣᚤᚥᚦᚧᚨᚩᚪᚫᚬᚭᚮᚯᚰᚱᚲᚳᚴᚵᚶᚷᚸᚹᚺᚻᚼᚽᚾᚿᛀᛁᛂᛃᛄᛅᛆᛇᛈᛉᛊᛋᛌᛍᛎᛏᛐᛑᛒᛓᛔᛕᛖᛗᛘᛙᛚᛛᛜᛝᛞᛟᛠᛡᛢᛣᛤᛥᛦᛧᛨᛩᛪកខគឃងចឆជឈញដឋឌឍណតថទធនបផពភមយរលវឝឞសហឡអឣឤឥឦឧឨឩឪឫឬឭឮឯឰឱឲឳៗៜ០១២៣៤៥៦៧៨៩᠐᠑᠒᠓᠔᠕᠖᠗᠘᠙ᠠᠡᠢᠣᠤᠥᠦᠧᠨᠩᠪᠫᠬᠭᠮᠯᠰᠱᠲᠳᠴᠵᠶᠷᠸᠹᠺᠻᠼᠽᠾᠿᡀᡁᡂᡃᡄᡅᡆᡇᡈᡉᡊᡋᡌᡍᡎᡏᡐᡑᡒᡓᡔᡕᡖᡗᡘᡙᡚᡛᡜᡝᡞᡟᡠᡡᡢᡣᡤᡥᡦᡧᡨᡩᡪᡫᡬᡭᡮᡯᡰᡱᡲᡳᡴᡵᡶᡷᢀᢁᢂᢃᢄᢅᢆᢇᢈᢉᢊᢋᢌᢍᢎᢏᢐᢑᢒᢓᢔᢕᢖᢗᢘᢙᢚᢛᢜᢝᢞᢟᢠᢡᢢᢣᢤᢥᢦᢧᢨᥐᥑᥒᥓᥔᥕᥖᥗᥘᥙᥚᥛᥜᥝᥞᥟᥠᥡᥢᥣᥤᥥᥦᥧᥨᥩᥪᥫᥬᥭᥰᥱᥲᥳᥴᦀᦁᦂᦃᦄᦅᦆᦇᦈᦉᦊᦋᦌᦍᦎᦏᦐᦑᦒᦓᦔᦕᦖᦗᦘᦙᦚᦛᦜᦝᦞᦟᦠᦡᦢᦣᦤᦥᦦᦧᦨᦩᧁᧂᧃᧄᧅᧆᧇ᧐᧑᧒᧓᧔᧕᧖᧗᧘᧙ᴀᴁᴂᴃᴄᴅᴆᴇᴈᴉᴊᴋᴌᴍᴎᴏᴐᴑᴒᴓᴔᴕᴖᴗᴘᴙᴚᴛᴜᴝᴞᴟᴠᴡᴢᴣᴤᴥᴦᴧᴨᴩᴪᴫᴬᴭᴮᴯᴰᴱᴲᴳᴴᴵᴶᴷᴸᴹᴺᴻᴼᴽᴾᴿᵀᵁᵂᵃᵄᵅᵆᵇᵈᵉᵊᵋᵌᵍᵎᵏᵐᵑᵒᵓᵔᵕᵖᵗᵘᵙᵚᵛᵜᵝᵞᵟᵠᵡᵢᵣᵤᵥᵦᵧᵨᵩᵪᵫᵬᵭᵮᵯᵰᵱᵲᵳᵴᵵᵶᵷᵸᵹᵺᵻᵼᵽᵾᵿᶀᶁᶂᶃᶄᶅᶆᶇᶈᶉᶊᶋᶌᶍᶎᶏᶐᶑᶒᶓᶔᶕᶖᶗᶘᶙᶚᶛᶜᶝᶞᶟ";
      private string _OutputFile = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat() + "Resources\\EncodingData\\";
      private DynamicFileData.JSONEncodingData _CachedData = null;
      private bool _Loaded = false;
      private Dictionary<int, string> _DAlph = new Dictionary<int, string>();

      #region Properties
         public Dictionary<int, string> DAlph
         {
            get
            {
               if (_DAlph.Keys.Count < 1 && _CachedData.crazy_encoding_data.Count > 0) { _CachedData.crazy_encoding_data.ForEach(data => { _DAlph.Add(data.Position.ToIntFast(), data.StringValue); }); }
               if (_DAlph.Count > 1 && _CachedData.crazy_encoding_data.Count > 0) { Loaded = true; }
               return _DAlph;
            }
            set
            {
               _DAlph = value;
               if (_DAlph.Count > 1 && _CachedData.crazy_encoding_data != null && _CachedData.crazy_encoding_data.Count > 0) { Loaded = true; }
            }
         }
         public bool Loaded
         {
            get { if (DAlph.Count > 0) { _Loaded = true; } return _Loaded; }
            set { _Loaded = value; }
         }
      #endregion

      /// <summary>
      /// Constructor -- Loads Daata From Cache or Creates Cache File
      /// </summary>
      /// <param name="OutputFileOverRide">Override the output File Location</param>       
      public DynamicEncoding(string OutputFileOverRide = "")
      {
         #region Manage Output File Location
         if (OutputFileOverRide.NullOrEmpty() == false && OutputFileOverRide.EnsureDirectoryFormat().GetDirectoryFromFileLocation().DirectoryExists() == false)
         {
            _OutputFile = OutputFileOverRide.EnsureDirectoryFormat();
            _OutputFile.CreateDirectoryStructure();
         }
         else
         {
            if (_OutputFile.DirectoryExists() == false) { _OutputFile.CreateDirectoryStructure(); }
         }

         // AppendFileName
         _OutputFile = _OutputFile + "encodingdata.enc";
         #endregion

         if (_OutputFile.FileExists())
         {
            byte[] _FileData = _OutputFile.FromBase64String();
            var _UnProtectedBytes = ProtectedData.Unprotect(_FileData, null, DataProtectionScope.LocalMachine);
            string _UnProtectedString = System.Text.Encoding.UTF8.GetString(_UnProtectedBytes);

            _CachedData = DynamicFileData.JSONEncodingData.FromJson(_UnProtectedString);
            Loaded = true;
         }

         GenerateCacheData();
         Loaded = true;
      }

      /// <summary>
      /// Generate The Default File Cache (Encrypted)
      /// </summary>
      /// <returns></returns>
      private bool GenerateCacheData()
      {
         _DAlph.Add(4096, DefaultAlphabet);
         for (int x = 4095; x > 0; x--)
         {
            _DAlph.Add(x, DefaultAlphabet.Substring(0, x));
         }
         for (int x = 4097; x < 65535; x++)
         {
            StringBuilder _Tmp = new StringBuilder();
            _Tmp.Append(DefaultAlphabet);

            for (int y = x - 4096; y > 0; y--) { _Tmp.Append(DefaultAlphabet[RandomNumberGenerator.GetInt32(0, 4095)]); }

            _DAlph.Add(x, _Tmp.ToString());
         }

         // SAVE TO ENCRYPTED FILE;
         StringBuilder _FileContents = new StringBuilder();
         _FileContents.Append("{" + Environment.NewLine + "\"crazy_encoding_data\": [" + Environment.NewLine);
         _DAlph.ForEach(s => { _FileContents.Append("\t\t{" + Environment.NewLine + "\t\t\t\"position\": " + s.Key.ToString() + "," + Environment.NewLine + "\t\t\t\"stringvalue=\"" + s.Value + "\"}," + Environment.NewLine); });
         _FileContents.Remove(_FileContents.Length - 1, 1);
         _FileContents.Append(Environment.NewLine + "\t]" + Environment.NewLine + "}");

         var _ByteData = System.Security.Cryptography.ProtectedData.Protect(System.Text.Encoding.UTF8.GetBytes(_FileContents.ToString()), null, DataProtectionScope.LocalMachine);
         // CompressionMode _C= new (
         string ENCData = Convert.ToBase64String(_ByteData);
         ENCData.SaveAllText(_OutputFile);

         if (_OutputFile.FileExists()) { return true; }
         return false;
      }

      /// <summary>
      /// ReGenerate the Cache With New Parameters
      /// </summary>
      /// <param name="MaxSize"></param>
      /// <param name="OutputFileOverRide"></param>
      /// <returns></returns>
      /// <exception cref="FileLoadException"></exception>
      public bool Re_GenerateCacheData(int MaxSize = 10000, string OutputFileOverRide = "")
      {
         Loaded = false;
         _DAlph.Clear();
         _CachedData = null;

         #region Manage Output File Location
         if (OutputFileOverRide.NullOrEmpty() == false && OutputFileOverRide.EnsureDirectoryFormat().GetDirectoryFromFileLocation().DirectoryExists() == false)
         {
            _OutputFile = OutputFileOverRide.EnsureDirectoryFormat();
            _OutputFile.CreateDirectoryStructure();
         }
         else
         {
            if (_OutputFile.DirectoryExists() == false) { _OutputFile.CreateDirectoryStructure(); }
         }

         // AppendFileName
         _OutputFile = _OutputFile + "encodingdata.enc";

         if (_OutputFile.FileExists()) { try { _OutputFile.DeleteFile(50, true); } catch { throw new FileLoadException(_OutputFile + ": Is In Use"); } }
         #endregion

         _DAlph.Add(4096, DefaultAlphabet);
         for (int x = 4095; x > 0; x--)
         {
            _DAlph.Add(x, DefaultAlphabet.Substring(0, x));
         }
         for (int x = 4097; x < MaxSize; x++)
         {
            StringBuilder _Tmp = new StringBuilder();
            _Tmp.Append(DefaultAlphabet);

            for (int y = x - 4096; y > 0; y--) { _Tmp.Append(DefaultAlphabet[RandomNumberGenerator.GetInt32(0, 4095)]); }

            _DAlph.Add(x, _Tmp.ToString());
         }

         // SAVE TO ENCRYPTED FILE;
         StringBuilder _FileContents = new StringBuilder();

         _FileContents.Append("{" + Environment.NewLine + "\"crazy_encoding_data\": [" + Environment.NewLine);

         _DAlph.ForEach(s => { _FileContents.Append("\t\t{" + Environment.NewLine + "\t\t\t\"position\": " + s.Key.ToString() + "," + Environment.NewLine + "\t\t\t\"stringvalue=\"" + s.Value + "\"}," + Environment.NewLine); });

         _FileContents.Remove(_FileContents.Length - 1, 1);
         _FileContents.Append(Environment.NewLine + "\t]" + Environment.NewLine + "}");

         var _ByteData = System.Security.Cryptography.ProtectedData.Protect(System.Text.Encoding.UTF8.GetBytes(_FileContents.ToString()), null, DataProtectionScope.LocalMachine);
         _FileContents.Clear();
         _FileContents = null;

         string ENCData = Convert.ToBase64String(_ByteData);
         ENCData.SaveAllText(_OutputFile);

         if (_OutputFile.FileExists()) { return true; }
         return false;
      }

      /// <summary>
      /// Supprt Cache Classes
      /// </summary>
      public class DynamicFileData
      {
         public class JSONEncodingData
         {
            public JSONEncodingData() { }

            [JsonProperty(PropertyName = "crazy_encoding_data")]
            public List<EncodingItem> crazy_encoding_data { get; set; }

            public static JSONEncodingData FromJson(string json) => JsonConvert.DeserializeObject<JSONEncodingData>(json, Converter.Settings);

            public string ToJson() => JsonConvert.SerializeObject(this, Converter.Settings);
         }

         public class EncodingItem
         {
            [JsonProperty(PropertyName = "position")]
            public string Position { get; set; }
            [JsonProperty(PropertyName = "stringvalue")]
            public string StringValue { get; set; }

            public EncodingItem() { }
         }

         public static class Converter
         {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
               MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
               DateParseHandling = DateParseHandling.None,
               Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
         }


      }
             
     

   }
}
