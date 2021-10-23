﻿CREATE TABLE [dbo].[Menus] (
    [MenuID] [int] NOT NULL IDENTITY,
    [ParentID] [int],
    [Text] [nvarchar](max) NOT NULL,
    [Controller] [nvarchar](max),
    [Action] [nvarchar](max),
    [Params] [nvarchar](max),
    [Keterangan] [nvarchar](max),
    CONSTRAINT [PK_dbo.Menus] PRIMARY KEY ([MenuID])
)
CREATE TABLE [dbo].[RoleMenus] (
    [RoleName] [nvarchar](128) NOT NULL,
    [MenuID] [int] NOT NULL,
    CONSTRAINT [PK_dbo.RoleMenus] PRIMARY KEY ([RoleName], [MenuID])
)
CREATE INDEX [IX_MenuID] ON [dbo].[RoleMenus]([MenuID])
ALTER TABLE [dbo].[RoleMenus] ADD CONSTRAINT [FK_dbo.RoleMenus_dbo.Menus_MenuID] FOREIGN KEY ([MenuID]) REFERENCES [dbo].[Menus] ([MenuID]) ON DELETE CASCADE
INSERT INTO [__MigrationHistory] ([MigrationId], [Model], [ProductVersion]) VALUES ('201608211040225_RoleMenu', 0x1F8B0800000000000400ED5D5F73DB38927FBFAAFB0E2A3DDD6DD55A4EAEB23533E5EC96339999CB649C7863CFECA38B9118871B8AD28A542AF96CF7701FE9BEC281A248E24F77A301827FE4F8C56511400368FCD0DD00BA81FFFB9FFFBDF8DB97753AFB1CEFF264933D9F3F393B9FCFE26CB95925D9FDF3F9BEF8F0E7EFE67FFBEBBFFFDBC54FABF597D91F75BEA7653E5132CB9FCF3F16C5F687C5225F7E8CD7517EB64E96BB4DBEF9509C2D37EB45B4DA2C9E9E9F7FB77872BE880589B9A0359B5DBCDB6745B28E0F3FC4CF1F37D932DE16FB28BDDAACE2343F7E17293707AAB337D13ACEB7D1327E3EFF47FCFEACCA359F5DA649245A7013A71F1C9B73FE7DD99C795391A8EA27D1A4E2EBEDD76D7CA84E90DD6FB76912EFE45C22DFEBF8ABF2417CBADE6DB6F1AEF8FA2EFEA0957DF5723E5BA8E5173A81A63850B66CCEF3F9ABACF8AFA7F3D99B7D9A46EF53F1E14394E6F17CB6FDCB0F37C56617FF1267F12E2AE2D5755414F14E0CD0AB557CE8CE912D3F6CFFC2E3CCF78BF3A725671651966D8AA810A36D74406BAEF81BD50DBD29760238F3D9CFC99778F55B9CDD171F9BC65E455FEA2FE2DFF9ECF72C113813858ADD3E063A47D77A998A6A8BA0F556BFE96A6FE334DE963C19BADECDB50D0C3481D7B1004694DD477DB7FD62D14E24727AFDB8CF8BCDDA6F7AD5657DA6975CF6614EAF27E7E7A34CAFA7CF9E8D32BD9E51DDE554FB5A0CEEFD6697F0A1F15066ABA8FA4DF439B93F40D1C295F9EC5D9C1E32E61F936DA576CFEAC43B33F7CFBBCDFADD2695A69C91E9EE66B3DF2D459B6E37B69CB7D1EE3E2E3C658DD1380F990381C455F6B803ED1B9641939804F548E57DA0BFC6B41DFDF53CF142FFAF7196E42FA292973EC0978AFB605E2BFE30E11EC0A29D04DCAB7182C15EA5DDC96892506EA61AF006B274C2B53FA4FDD17C5240769A7A34A9DF369FA23CE94A659C99F5F792D95D9AFDDF02C8D18B384D0290F9458C6CB20B40E82AC992F57E1D80D2F52E59C6BF25791180D6BBA88824092646389DB628D4AA7E95BFD964979F8AE4435DF58B8D105E654B6C9D4085AA2231430856DD6A26642FB78DD5FCA69A57E7305A5625608D3AA6BAB6E73A5EBF17132ECA5EC64594A4B046D232DD99CC8373189A09C9066927BAD1D93FF7516A6BB492096C349403683498AD934A3D0EB2874A6D1584AB4AE5AA9649A8D447DBB08B8C402C424D827821579BC23E106E4898280E6C452A3575D1B9618CBB5FF7EB34FA1840FB3B92B04B7F96D89772E392BFF96D282A3CA7ABD22274BCB7AAB2B596D0F7EE7327F0AC093A112621FE6F05AFEFA3B46EEB4BD18EDB64ED2ED0F9677C96991B15FB8FB7F17ABBE9DCA463D75E445FA31D458CB58F9F6CE372684B5AE4D8F2CEBE8A96E37E6CFA3D8F77E57F832F27465CC9DC0844EFF35B517B1E7DCA5D97AEA88C6B4EC5492977D76603245C938A4BB736CB202B074715625B3F98CA86DB7A75E65898AC678658ADE62118AE65ECA450B4867968155382B8AA16B60C9A927EF98697170D122D33D607F5E87CC5A687F38ADFB2D6271B0DE7C1D7FB9C463BD87E47AA7EB6DFB1B09FED27153E81B919CAF60BE565F068FB3DDA7E48D53DD97EA427482B9EA04370331530458C2CAEB61FCB7AEA2092F12633ACA7E03BC7ADE826368FA5265AF68FDB9C61144A978D3854AD04DF8863EBA087BC11374D7BB04177F8B9804E6474D604D903F43CF9B1B536C41EE0EB287F1D2FFDE66B5DD6CF25AE2D7B0246E0E18C95DFE6612CCA0056D2E8C64AE0E3F606CED044AC13EFB4ACED448473181311C9D66922AA6DF2F5D7EB32251D313E897939D0C609E12C5DB10BB6DCBC01A7DB6C165C7A014E0CDEA7CBD53F85CDED15E9D494F68A75524A9F00CE4289EC72559A3DF455297E88D00C3BB5DED173DD494895A60E91CD983F54DE4093C87FD9434DA5C0EB1E977937DCC2E7A5B069D7624A755CBE54C4DEC5CE361103AF7D0155B72C38A00EB02431AA31D72448167B8B83AC4AE2AFD751E217AF5315F55A93B445DD825E9E7ED7F528E68F28DD87550A40B56CF6BF8B8BFDAE9377884AC167304C0ADF909970CAFBDF0C1F17D611C054CF26465F320FEBDBA1CE43D0C103C962A80A2C5F0FDBFD5A55F89E3F99D1D6838EBBFF2A35CA248672DE69521AED1498DD309139653A99CA50057DA816576FC5C026B6ABDEA267FCA3C7304422C9859DB60AB775A8CFA46166202D5CE8591BC6A31AAC91DF1FC7AEF4E2600DD664AE66886CBCC68758D51C097770BC512974104FDFA60BCE299BD50CF7A147B37A526635E936A3CE43D07706C982C9AB21BC68B4AAAC6635D39F86CEED6956333C6BA09C779A94463B0566C7CC6AB24C08B3BAB3F38D4DB58CECB1E3AAB76C66F5A3FFCF606635E5CFD3C30CA4850B3D6BC338298135F2FBE3D8955E7C96C09A50B39AE5BD44E5ED64565FC5D9DE47E295E57C44515DEE040CE7EB6827EAEA6A32C65FC2DEB8C97397DF64C56E93A6F16E707BF07259F176E06AC56045EB7CF06A27E119508A88725E2176DA31F5EE30D565F9232798B69792DACDCA3A92F2B2AC44D96AC9C8B0907C85525BC9D047973C71C8054235C2FE1830B40E88100E062EF37CB34C0ECDD01674E6B57F6AE77ECA5633F60D98D568C9B70A8A01DAA745B24D93A568D6F3F99F0CEE712A6826435B81795BA15AD193B98ECFB7424FA7423ECC2A9128EA8BF265B432C753F070A57E11908E4BE59344A910E579B18B92AC30F19F64CB641BA5DCEE6804600F00EB8D9D65839BAAF59497F136CE4A15CD1DC3706D6AAAD6986BE3E5C542022B8D61E0D2290C5BD40D542DA86AA3908F59EACAC096AE722DD6D4708A77810306F2664D276CE24314A41DC3E111BC80CA808C7EE5580014EA3714B524EB4B8D268A3DB5E19CE1C6AEEAF2419C3A125D6B1F0067800703860CCA93A18587744CC6071DE1FC20916E7D2CA6863DBCFD1C08E0AF633841101F9FEE8D180489C6A63F0E177CF35F4662B3B3E48244F4BC8061924E018958FB392008641AE2E3D3BD11832291BCB805020D76A0130C95D8A5186D05FAE1935ACBF9D9D913A3A28E4883DBC4196ADBB5369EA883472154838644A0B1496E4508BE590E60F0782CE683443C9E9E81F509C949AC1F1CB4D0C77C1EC8C5062F40634640AD651D6D3B1FE905AFF8C27AAA6B6ABA071C64045952D3A3D5B51983AE74D85ADDE6FD1C6CD53325AD4EB76904AD4E8FC2496975CCA3D48A10DCB314C0A08F94B45EEDC8C0FA1464A5A51F3C454AF9C47B20171BBC008D1901B556AD4E3B13F782D753D4EA540F06D4EAD4689D805647AE92C08063BBC8443E14AC6FA5E0C3D2766B85768A8355313E3AE98E706061B975C509A4F4A0056ACD0058A5E2E23148B182E4A52D72FDDE0507F4B22E8E80AA9A1E7E195D61ED89D33750F011CC18C510ED1903C31673C07A5D424FD83D3D8BC0D285C14C02CB889D804D80856663E0B1C669B7E0D1231BF908B5057933CE39C3ADF22D8D61C9A230879616DE776FC9F078636E2F3123EC7BC0DE74769B580D1B7CCB89353227B4EFC489D1E641C712B08D61D55D9F3BDDD4C09D22E3EB78876E71F0C5B8F2C117F5E448876ADB58D8F784FDF0883FD57D585E6786DC8CE50DE869ECC852773B38C10B5F8BF582E6D35B9331BA31D8BA8C3182A7B33663BBFA5983FD0DCC7AB857D96E0A6078FE05B78927E0C667E17DF7960C8F37B7B519DFAB2F18F626B7369B98931F6B644E6F6D4606FAF3A06389FAC7B0EAADDB79D77D70A7C864743DA75B0EEB9F300E820E231DAA6D6361DF13F6C323FE543D5F799DE1AD847A44F789FAC052178438C1CBBA360B8BE6935D9B4DC239963182A7B03653AE1240E103DF2B20E1B3BE13C20593F085152D5188E0043008359B33D2F075166E9883C6A15BDDC13156DD20515E59234AC4BB6303FE11BF2F3F95D7E7E87828B3DFC48576B297CF67ED5D14C621A9012A9548BD048588B4AB790B11FD9E028898798D8485A814670ED15322FD2DA4702A4C0255183244A08EF7B610D076A6204AC67E22972449CC4A465DE941B4F425B5B55D47410FB7ABB1FCB864288E695ADE0AD3E6D92D009EB5B72107961425CD79D142AEF52701A7B1E445C6268433CCF421B271AC7ADB0464D8F1B1151B09756316A2A41F4B3A11C43B0B6FD2F3881308D617EA4E04ADAD7503F4F1EE2B8358650ED85AD65E9E65B6A631523422922633758879C3D14CCAAF2915CB75488AFD63BF10A9E99CA2D10C1DEE7205924412D06FBAC5A43286C134E04A1D805BB68B77943E1157EF489D699421C11DE2B21D8992AAA14371A4D6A82833A05B5FA0D66BF7BEF8B140BBE94522D2D8049D3B0E38E301BDB7B9EC29AD27FCF5A42EC862996005E197271193CCD1001C31AFB88638421F94699D404FC9148EB4A29FE4087A1AC691449D38A29B63145FA8031DA443C8694E171E21A7361249C3080DC72FF3225D8261F42E2BDC3D748B15625963007018876EA5B2062300D370BDC4D9C3233B85EBA74EBCC29555383D85FAB29242DB612A5A9C5EBB08F071A622FA540CC130DA190DEE1EEA8906B18C072F9BC7196B3002308D9A8A765727B253D454ECC0AB21A622F654B6C9254E24A4D21D4B2CA4B23A68360308F658A21F75B31A25EAC125F2C5599355EC403CA57F9C503CD95A34B72B08E67182EF40E2BDB00F9F8BACF82FBA67F86CECC8B221E623165B04B08A1586049DCC711636C6A613C1285BE8514F4B1C3A2CC6CE30B63DC18BA409C3BC31CC0B4EFC86959D8CB00FA2DF74E007CA5ACE347609F5E08F612836BB73D89BB943F07538F38E7252E7B2D2AA5B385EEDE19938A09EE16C12B15CAA090F1072BBC8382EB0336BE88D23DAC5D7CE30573DC3DF47EAC0BC11F50CE98B6A6527C38595E837EDC48AB2D6612AB3DC56F963188ACDEE1CF666EE107C1D6E478F72B8E3B292AB67B81B7C819838889E517CBE2086E14E616A0F40B7309929ED692CC508D0114C220392203A5E3F63D338213569178B9BC3B362C70F170B9165196F0B3104579B559CE675C255B4DD2682DD6DC9E397D9CD365A9647AD7FBE393E5AC67BB1ECBBC5F9F78B754563B1CC65AEEA2E534D4DC56617DDC75A6AF91CD82AFE39D98935705444EFA3F249A11F576B239BDDE5AAAE48F7BC3287AA3E49AF4B94FF378E5D6715EBCEB0D56ECBBC9F457FD6A53BDBE13938600D6A1615856F966282ECF4876AA51B207EDCA4FB7566BF1902A726FE462A9DEA0B9FC2652A0A142A8DFA1B9FCA6D9CC6DBF2BD35994CF3D181CEE65AA3517EE097971F4293C9C8DF4D6A170B6D9C75202D0C2469535A87260BB8AD61E9095CCC7C6600172F8AB1560E8F93594B85CDE1D41E1A70A1C7A11400321E8F225AF92D4D0BD3BFC7737AD87C9A18D3C44E621C40749F3E270509C5B7C9130D84D716030864698CC7DAE35C329B2DEF763D8EBCE62CEE39E8DEE3ED3AD4F028FB0C701FA0691FEE92C9E1CF79E194BAC3EFEFE5ABC93281C3077EF9C323E92FE23451A9489F1D69FD22163DA5EBB441AD4E70A4779564C97ABF060836298E14AF77C932FE2DC90B80A694E648F59D5885E9324049E85BA460D45EE56F36D9E5A722F9A05293BF4F4640D58EA79E020A769F650828ACE029888093D240C6198CE748EBC10FEE436EA580F15BB9064A6638793F144E2FA0B2DBAFD3E8A3A6E58EDF1CC51920C8A688A2EEF8E9829CF130732BB0711FA5DA32B6FEC8A71376FFECD7A8D87FBC8DD7DB8D8641E9BB731F5F94C75C60478F290E148D9B5F14AAD67B6108CA9BC2188DEA139FC6EF79BC2BFF53C9B45FC732206E0A317CF96D79D4187DCA3543D1489C8C8CD04F493D05057D46CC90163602C383F59B33399A43476F65811CB8B294055A165716D25D26AAB2202E39219014485984DDB37E54168FCA627A32A2F3B2448D72EE202FDC972561A5C6292F4B1E84DE6AC324BC4F6CE04810D6490D56143FA169DF58524F66F0B79708C8E8CF36191BB77E7443A9C21022BC0FC10BC9DB09415A0BFFE972F6D401DC96F24363926F8E8F346A72C891E790E161568CF1A20AE3D3417E304B9D14D4535A38C550A2A3B4D132D072CB06B1DC4607514713CBB8E8A70BA05C8DACF0B00A6D66BD1429EBD22510B2B7A44457BAEF621DFBF2F7C9C0ACB9D5C9D7663A5EFDE461326125F1E97B2860184CED67FE18FD11A57B4DA21C3F4D6664F4B02DCF01A2C3D818E3642380B1D87CD646E6B6FDD11B9CF2948CD270FB0A618F164E697FE77147C598101D753E78DF5DD7A9EFAAFBFB13008F27CA761A492EB4E24A25527F9B1AE8BB9E37D021747CA03B9F3C98CF0300101FF50C625A3A2EEC89C8A38E3B4D1D17E6E800BC25B5EBD4F7D4713D0880C7E3093B8D93D0715518A627C8A1505206A8E162181BEB6BEE65366257DFE354AEA3F2E27B03ADCD5797A8A82F9AAF71F5C541CF6CB262B749D358D30BF2778758AFC313095AACD7F19B137FA2756E70E7F0ED81EE61B681CCBE32BEBE7BDA43AEA34551592E4A98664EFB953F486E13AAD7C13182BAF52C4DEDC72FCDEF26A8FB1850AD447A1F7A5EC66D1F7A9C1F83BBF508EB2ACB7C26D8F3395995D1D5375FF3225E9F9519CE6EFE95FE9826A2BF6D86AB284B3EC47971BBF91467CFE76500F87C76291674791563EF143BFEFDE2FCE9225EAD1779BE5254BA843E6DEF450BF97E1D7FD5C7C27C2404DF9FB958E8042E743B4E2A5B5DB99E94EC3860F997588C5654C4ABEBA810735BB0E355F9E04852C6ABBCD9A769F4BEBC05E04394E6E6A32DE089595541F639DA2D3F46BBFF58475FFED399D2E531C4D542ABD8995BA93AA926CC3504AD32205562A063AF64E1E9DC1A797A9220435E7065810C5F20D94126971D1864F3D955F4E5B738BB2F3E3E9F3F393F0F843785ECD367CF02414F21FBECDC952A1453EB8FC99380341DA0CC83B63D14D90E710BEB4F02EAC38C181A49CC1B2C3206D43E4E5AF1935079C38C8BFF90F88FC62003810FB923A13658D09F4638541CC286FD1B22C50B77245287097724D3040777A423050477A454C70157647607B78921A7AE494E8E00AEC8BD4F6C9D630B0028A2962700B0285ABB0000A7D4B72E89C9F34EDE8890C7904145B85293FF840B219FEB2DE08ED39E4FC07D44038FA5FFF0849E63CDC16055C74A502D9272CFCA910EB21FE18A042922A96383D4833C9C186BD5671CDF815DE4ED37142DB77D58D41E9E06D04B81D59C717E187C4252219EBC59693B8AB54F4D120D8F3A107485E04A4CF480922331A5C2272031C36CF63C4ACC4789C99B905D8C52C26F20B0510ACFE16FC1281D6F27160ACFE2EEC06261589C9DD7B66CDFBB48485D2349FECE12AD1721D4C3CE0411FEE7B069DC0564D4E84FC55463B3138BCE639E361361538CF366A5F40998576D98DFD85ADF637CFD75351D1C175459A390184D5B4B41775D4955C176C1AD3130748DA96BB1683586AA6D8B52478F4FBF7366D631F4AD47A1470591F118678B17B1F3CFA4700202705AEB26EBFE206B2938CD15EAC9AD09EDD1596166D6C8471CE4B4759C0CDFF2718949A076950FBE62A0C2A99C30D96123D1A4F028ED1D4598756FF351DA0F2EED3B6F03DA639246DD3B24A7ADB3B47FDC871C40DA9B51163C24C211117688D4E57A94E76DD492BFDC3B842C8538F29383960288AA3A722900A93A7CE964F666E0A020A6E04403810262BBAD24F0121F98345EB35F7A3D52B312EE2CCEE0EDCB9546C663AB0EAF689A8957FBB448C4B273299A233A6FC492BD15DA3015F09955C8162D8AF265B4325922BAB4C2DA04B6056BC39F0CD262E4E3526424512AA66B5EECA2C48CE3BADE25D932D94629C607AD007258637FB9EB62D154A5A7BC8CB771568A45BDDFE1EA6EAAD0B86FE391F234298DB9E37BAFB8337B3BB0721E796C95EF83400C68045C7F2078D14F8E21C7347840B803A842563A1C9A400FDC76F88EC9F2F0D59F1E247CD0B781F81EC87D8186AA6F00BC687B6E77D8BE763B766D0E79F8A4AF832048DF2B04DB52A7F58229EA1E3AD73D463EB2EC176139D73D02CA2C7A8E2D1E1E2CB81C0448102DE703AB91355DD3E43B24E2BD1DCF26833C90EDC76131C5139B8170443CC80E9CD413B733FA602940A583E28874E56E4752CB260FA79EA48EE9F9D9D9136358C74688F58D237787F51ED1C2A97A10CC1C7763EFAC1B044E8BF1DE644F7D7CA522ABF9DA0BB29C96E381B600A88B135D2B1D1447A3CB9E311032BEEC7141CBD4644FB36283CFA6BD07B76F21041AD55A5A4F2B3697E1A6CF093D40E6B46223EB1E0165935FB18D0DAEE1576CEEB01A79C5567BBDDF515EF8DA6E7793CBD8EF6E53064118D810AC0D2177BD89C78E9CC3139C1016BAE20110A6FBABDFA1010AD2E6409B45D91E903E0F0230C3D71E6E4DAF428C7CAAC9D9B59F0F35C6BB3EEEB58F81B789ABC929606C6845E985AD9135A5EA271C7883D3B214D4C21B648A7AD283D9B2B43E2C34C17D4B0D23236F208C8D9AB1B7123C103491FD0428F4E28E8C710A30EA3DE937308C046F55AF7ACE03118CE8155F443AE83E4E2BC6C2E5A9B8274C0988E3382A78C36FA2C89BB8653F25C00D6DE17B436D1A567E3F47893C7B0D3A18D0931ECCE1A0F569B5099E106A189986953F1A6A2662E59FE0A921147277477527C4A8F7ABEBF0131F38439F56BE0B2218518BBE8874B7F2C73E670471792A47DA5302E23887DBDEF09B28F24EC3CA9F04E046B2F24FEED4BB8ED3BC038235DB913D24CA23597D1806557524A982A4E6632FE8415EC2E3469FBA21077F79CFA5BEE058F9497DCDAE0991D65E9E3330737C204F3B8898CFDA2857E304A87ABDEEF97CF5BEBC95B78A94AD538D371B8D2ADA35A651459B045551A7DAAB3043658DAACC2C50957A2E7BD54AF4A451AB920A552865B0D7855643D5C0255E87F019C4EB0488789566276E6C1D19B51839A0EAB44C0EF552355AEAB2D7A2AF8A8DAAF40C507D6A1E4ED71A7315E85A930677AD79679E5D0B31705A0EB246EEC0B5BE51C04CAE93E0195CA532672E518F968ECE5E7E8DB25790296EA54450E036E92E15A1C36666A12B650FDCF1464860DCEA1470D8AA443B7DFD64D0A846CF00D5A6E671AD14E5299CCDDE002E6FF5FD12AC01E4E457F3B8566AEB3B43164039EDCDA8AC49A3DAEA33544DF5DCB2B5778DA16AF6A849027BD1BCE8AC5721D96BA61164DE0D3293F26B6691E52211C50CC74CA0C313D066A261A04207051205ACA4629E8A9C0C56005756003CB05D6CA1341930C20EAD56BE135D068AC2A5FCBB5B5B51684FE15B06806B17A446D69F46EA1A7EBE0E74937918AF6DD46972FED07EE92BD171445CAB14EAB4E0ACC081CD391AF61BC12974BFF536A4BA8EF8242A1DD057A68796B71F395DE7A1A6537775CB9EEA347DFE441CC049BDD09346668371FA0C32803EA3F6573ED8A6BED2DDE66BC0EE72469D73E8DACBA80FC606E3BC85E2037D38D3A50B705950EC6969C15941497DFB514110A93F64F791902CA0FB9CE02DD3A2D317E7AD4DD7A610CC008B63253DBA4FC50B013C608717A96AD0D833A814A1F4996001B611A0D10886082CA485C38E9EA6C4D02CC0A22F0016B00235021945F0BEC9A1BC9E149A05766DE9108DD083CA1C8F35B0BFBC95410C37FB301D24A8009309CED00FCBDCB9D5F77A7352ECC1C52EDB3D3980E81D99259CC509CB8B36D00A05DEBE951911CE48A73D3FED8C1847048FC01AD899D1CA20860F64980E1254F00915DAD0B7F8D971B9D5F7E26F52ECB18AE0411684A3B144F15B8298803B36291D900F670E0DAE3E505DD68E74AA6E361FD95DAB2F7E6F5C6C9AB48B45752274FC207E169B5D741F5F6D56719A1FBE5E2CDEEDB3F26597EAD7CB384FEE5B12178266162F15979E26CFABECC3A6F62CD25A546731AEB42FA255544497BB22F9102D0B91BC8CF33C29B17178D1EEF9FC27A16357AFB2B7FB62BB2F449785584EBFCACC283D94A8FA2F16469B2FDE6ECB5F79882E886626E563386FB317FB245D35EDFE19B8901F2151BA3E1D5FBE28C7B2285FC0B8FFDA507AB3C998848EEC6B3CB6CAE7C853412C7F9BDD449F63BC6D761EAA1CBB789944F7E5C311471A6D79F153C06FB5FEF2D7FF07DD56B1BF62940100, '5.0.0.net40')