﻿Use MotorCross;
ALTER TABLE [dbo].[Barangs] ADD [IsNonAktif] [bit] NOT NULL DEFAULT 0
INSERT INTO [__MigrationHistory] ([MigrationId], [Model], [ProductVersion]) VALUES ('201505291511216_StatusBarang', 0x1F8B0800000000000400ED5DDB72DC36127DDFAAFD87A979DADDAA68646D39E5A44649C9B29D751C5FD6529247173543498C399C09C97149DFB60FFB49FB0B0B0E6FB875A30162488EAC179506041A40F741372E0DF4FFFEF3DFF98F77AB78F2254CB3689D9C4E9F1C1D4F2761B2582FA3E4E674BACDAFBF7936FDF187BFFE65FE72B9BA9BFC56E73B29F2B19249763ABDCDF3CDF7B359B6B80D574176B48A16E93A5B5FE7478BF56A162CD7B393E3E367B327C7B3909198325A93C9FCE336C9A355B8FBC17E9EAF9345B8C9B741FC76BD0CE3AC4A675F2E765427EF8255986D8245783AFD3DBC3A2A734D27677114B0165C84F1B565738EBF2B9A336D2A6255BD644DCAEF2FEF37E1AE3A4676BBD9C45198F2B958BE37E1BD90C0923EA4EB4D98E6F71FC36BA9ECEB17D3C94C2C3F930934C535658BE69C4E5F27F93F4FA69377DB380EAE6296701DC459389D6CBEFDFE225FA7E14F6112A6411E2E3F04791EA64C40AF97E1AE3B155BBEDF7C4BE3CC77B3E3938233B32049D6799033692B1D909ACBFE0675432FF29401673A7915DD85CB5FC2E426BF6D1AFB36B8AB53D8BFD3C9AF49C470C60AE5E936D4740EAFF52C66D5E65EEB2D7FE3D55E8671B82978D277BDEB0F2630E004DE840C18417213ECBBEDF3593B90D0E175BECDF2F5CA6D78D5655D86175FF6610EAF27C7C7830CAF93A74F07195E4FB1EE52AA7DC3847BB34E233A341ECA686555BF0BBE44373B281AB8329D7C0CE35DC6EC36DA9466F7A8FEF849CDFD2A5DAF3EAE636EC829993E5DACB7E982B5E9726DCA7919A43761EEA86B94C639E81C1D486C758F3DD0BE621D348A41504B2ADB07FA6B4C9BD15F8F1327F4FF1C2651F63C2878E9027CAEB80BE6A5E20F13EE1E66B4A3807B29273DD8CB6F9F7834712857BF2AF0D664E9846B7748BBA3F9A0806C35F47052BFAC3F0759D495CA3023EBDF05B3BB34FB5F0CC8C1F3308E3C90F98949364A3D107A1B25D16ABBF240E9431A2DC25FA22CF740EB6390079C0663128EC7AD0AA5AA5F67EFD6C9D9E73CBAAEAB7EBE66CAAB6889A913A0521534A60FC52ACF9A11DD4B6D6339BEB1E6D5399496951FA046555F6DDBF3215C5DB10117242FC23C8862BD4592327D5299A7CFA15826209BCE3AE18D4EFED806B1A9D142266DA37539348DD666EB64522B213B98D4D640D89A54AA691985497D9C1B76D111C08C50D2204EC89586B00B841B122A8A3DCF22859ABAD85C3F93BB9FB7AB38B8F560FD2D4998B53F49ED73B961CDDFFC560C159CD3D6682136DED954995A8BD87BFBB1E379D4781D08A350FF978CD737415CB7F5056BC765B4B257E8F4333EC3C80DF2EDED65B8DAAC3B37A9EADAF3E03E483162A47DFC681316A22D68A1B2A59D7DE52DC7DDD8F46B16A6C57FBD2F27065CC95C30446FB34B567B167CCE6C97AEA08E6B4EC5512DF7A9CDA6D170CD5758BBB5597A5939589A10D3FA413536D4D68B23C7C06439B38ED5621E84E152C64E06456A988355513588AD6921EBA031D997AF7879D120D130625D500F8E57687858AFF80D6B7DB4D1FA3CF07A9FD2688BB95F45D56DEE5715769BFB71850F606CFA9AFBF9F232789CFB3DCEFD80AAF734F7433D415AF5A43B0457BF6AA6224A16DBB91F69F6D44125C34D26CC9EBCEF1CB7AA1BD93CE69A68D83F6E73FA31285D36E240B3E27D238E6C831EF246DC38E7830DBAFD8F05702083A3C6CB1EA0E3C98FA9B53EF600DF04D99B70E1365EEBB26E2E716DD9039804EECE58E96DEE6746E9619634F864C5F3717B0367DD40AC3F7E92B2B603519F43198840B64E03516C93ABBF5E97216989F1518CCB9E364E1067E9925DFA999B33E0E4399B01974E8063C2FB7CB6FC83CDB99D6E3A35A59DEE3A09A50F0067BE5476B12A4D1EFAAA143E4468C48EAD77E45C9F38A4724307C9A68C1F2CAFA741E4BEECC18692E7758FCDB8EB6FE1F382CD69576C48755CBE94C43E86D67322025EF7055479664101B5872589528DBA2601B2985BEC655512DE7F0822B7FB3A6551A735495BD4EED2CBC9B3AE4731BF05F1D6AF51D0540BB1FF2CCBD68B68871169C74FBDE022F6E265B29C90EF7A957DE3EFCFB0EE6CE33CDAC4D18235EB74FA0F854D940A1A65DF56A0DECB112B7A329581F49E2DAA63662327678B7C77D7FD3CC816C152151CE3E1524C61D80BD3622612C4E76C94E5691025B90AD42859449B20A6764722002CC04D77D38A063755CB5F5E849B3029E6505419FA6B5353B5C45C132FE7330EAC388635EED510B6305FEB1654B58EA46316BB1CD3D2151CC0C78653B80B1430A077C8ACB0098BC84B3BFAC3A3D6D55A818CEC5CEF0185B22F6E4BB276DF1D29F6C48653C40D39A5BB204E9444D7DA7BC099C6190B4206E699D5C2837392A2830EF1E8E248B7EE6263C31EDC7E0A04E07760AC2008CBA77B237A41A272340CC3053E27E691D89CF6D820113C5F264C49C78044A8FD1410789A1AC2F2E9DE885E9188BA28EA4003B902784325E4FED556207B2C88B51C1F1D3D512AEA88347D9B28A23639703AA24E2F055F0DEA1381CAC1B51121F029B60683D536A40B1261CF1102D647A427A17E50D082BBA0382017129E87C60C805AC33ADAE4C7B017BCC20BEBB1AEA9F11E5090E165498D4BAB6B337A5DE990ADBAE97A84B755CF98AC3ADEA601AC3A2E8583B2EAD07D222342E0FBA91A0CBA6849E3252602D6C7A02B0DFDA01952EC86B7037221E17968CC00A8355A75FC86F25EF07A88561DEB418F561D93D6015875C0690A028EC9658F3F14ACFDAFE8B034F96749A7385015C3A313EF08051606FF422B90E242F3D49A1EB08A7980409022B983705BE4B28791057A492E52BAAAC6875F4257487BE2B8AF151DC10429FA68CF1018364C078C8E417BC2EEE1CD080C5DE86D4A6090D8E8E604A597122BC31A9B8469D584DFC3AB2229BC53556B91FD22CCA573AF6C3A69FD9D94034705782211EE016085487B56642022FBC2E888A9AE4A06A29C2F838E9EE04D622005532112288FBA75046A9F020301F5D50E8592B2A8A092448919C9883B0C3A5AF2068EB15DED9B019A7635FBEA543218C7A4CD55234C9B4B0C1A78D6335A0A2C314AD204D940AED559DA61CCCD54C8846086A976CAC4B1D25354CBB0CA755526C1E93A55CB681E12E7F21B5F1D9755B28D5B66D33541E7295ADEC6119323A9D180B2AD151943609AEEDD54955B26F73FA14F880320D799465D22DC415CFE384AA20EF7C5915AE782CCD0F99EE95A2F799FB9B140F237E3883456A373C7750F4FA9BD377944015BF88A4F14D705DEC820AC40BCA03862DC84C50347D4E718741CC13D73F4076EAA6F8EC091D6BCA11C01BD71289AA8134764838DF1053B51023A049C2975E111708AC49154A629FEF8A55EB1471886BB35E8BB073A36E858D6586F0AE340570692303C300DB64B942375B453B07DEAC42BD858F9B353F0635E98D2B6188A86E3DD2E0A7C98A1083E5788300C3F8BB43C8DD4B18C062FD3F92349181E98860D45F33998CD49982F5EF53114A1ABE92A9728E731362732C2EAA0592E22EC319CC1C8D36A90A80397D01B9E2AABC8C701D60702FC6C515DD022CCA31C016889EF857DF05824ED425BED43FB6399CFF158DFC16C76379B6FF35919F5B64A98CF80F0B8F3B7C16613B15ADB9255CAE4A28C957BFECD857D5CDC554963B6C834E1719BD63635E5EB34B809A5AFAC6AD6D25751CA5817E4C15550DC403D5FAE946CE6BDDCBA22794B57155BBDFF529728FE6F768CAB98C147D0C2AF65DE2BD69F55B167BE7B6C42B33A538B4E8A40C56C0A9522AF7C9FAFE3ED2A315FC880A9954F9CF074CA143A853AAA274FA34EA353698274F2649A440B3A45944C814691402FCFBF64C193E1D3556AF3992467E518434192722624429304DC7621EB085C687D4E002E5C14622D7F4F83672D767F03A6F6D080ABBBD92C009070F31969E5D7342CD46D61C7E161DA0A270C1333896100D17DF81C1424842D714734209BFD0420A0A5211E4B37CB79361B2E9D3F4A5E3A857614BAB3BC6D45AD97B28B80F7019AF6D6394F0EBE8B0E53EA0EBF5DE44D9EC02E815E9E0BBCC953E1922D69D5D137156AF5074B7A4D104E8560F3C59222178C53A1C97DB3A45A87E55448D61FF6AD52206A7CBC4D9E1A9F3E1A05559F573A2A28FDA92B414141050F41051C940552766B1D252DBB2AD98BDC4801E2B770AB8467387ADD04A6E7D1D8558F9B0B56AE4AB354671A4536461475C74F17E40C8799E6915461195B27D2E9F8DD3FE3E3A50818E4D2ADFB58854CD174B4FA624151B9CA2850355E74442897EF838B9B0AB99D2CDA302A3C993675A8098412EA44008AFC71343A423E23765414F89138415B9808F40FD6AF6ECAD1789F381B0BC0D586642CC0B2B0B1E01EA3108D05F24A0582244FC6C2EF9EF5A3B1783416E3D3119D97254868A61D21B2BEB05F96F8D51A87BC2C791076ABF5AE713EB1D13B10914E6AA0A2F0094D7B41583C99812F0E239091EF1C2B1BB76E747D99421F2A7C1F8A57A76F470469C96BACCBD95307701BCAF78D49FA747C20A9F19E6A8E2283BDF308F2C20AC3C381BFED2D0E0AEC1E384CD197EA68E3D2C833B7A49799DBE020EA38C5C2E3C05802CA7692E51F56BEA7595C841775BEC57DB4A55B067B514996E9A38159735DD475CEA40B8742021558121EBE4D0C1471F4025155305A5550139E4E95D4B36414A75B394B537B95D2FC6E9C6E2B8757C11377D7EFC2AF76D7DFAC72BE953D60CB2CD30963CE97685978BF5EDC6779B83A2A321C5DFC199FC711EB6F9BE16D9044D761965FAE3F87C9E9B470D09D4ECEE228C84A5F692BDFDE26D659962D0595A28965A57F2E9E16C30ADC9A2704A5E2CA9617A9A3821D17A6A06F962176DE71A102932F41BAB80DD2BFAD82BBBF5B53AA5D100DB44881CE6B37441FB40A87418E819D43815AB4861CD90978099E0432784BCF0C32BE6CCF201363451D1F7BC29B40F6E4E9534FD013C83E3DB68E33A8F17974C7E44140DA10238B184AD9E42A4A09A98CB2FE20A0DE8FC4E09850F448BBEE4118A5E20761F2FA918BBB48DCA5D18B2060915B126A9DB9DC69F843C5CEADD3BD219C3F674722B51B6747328DF366473A9CC366474AB59F664926DD2D6BFB1CBA2A39DE43B3247715993A475600DAE06824050079399A158076487DED9A18F543A449047FE7DAA70A176A721F703EF4737D82D871D8D309D84BD4B32CDDC5E37B8C4931DA978C6AEE12A31DD88FB04502E731D2B141A2B3084C8CB4EA535C44B45DA4ED37E42DB75D58D43A8678B04B9ECD9CE215E27D40A2314B48A3D2E4EE631E9A281A1E6DA03E4417516382FE2D148DC9153E008DE967B3E751633E6A4CDA80EC3229C542AAF99D94EAC7F0D730291D6E27561B2C84B8030BB9C950765EDBB2FBDE4502EA1A48F377D6687B51427BD899C082D1D0378DBB800C93FE58A66A647682715268A7CD885B0BE1BC59287D00D3ABD60D6B68ABEF205F775B6D888DE2D358839018CC5A734E515D4995CE50DE67635AD722A2AD85BC8908A6B62D8A1D3D9E3CB36656E59AE44FE9A9A176882FF80B6F5FCA7101CAF601A143F61801470D0020B4056A831AE2C7317C99F19D279743F5894D189D3DD4DD437827531C044EB04244054EB6427A2F10D334425FBF2778E10F47597A005881CA67A5FDA1497B4ED78AAF0E44C189AF4E7A90F0015F78A19F53EE0B34587DE30E903C408462F049EE6E516EBB85931D32C2F1C30C684C560F0F165C160A641F4189C7177210C1141034887F30BC893DC409B24DEC175334B5E90947C8B3DA367704DCB0E4A1D25E71841EF8B6929443A370E2943F89323D3E3A7AA288756884185FAAB13FD6DE235A2855F782193C3C97EB627C6FBAA789102520AB49DD0BB2AC96E39EB600B037776C2BED154783EB9E211032BCEEB141CBD8740F1ED1CD5DB8FB5642DA49B5F46D4F2B361B71E39E080E20B35AB1A1750F80B2D1AFD8860657FF2B367B580DBC62A3042A9377BBDBC067F27E77FBA51784691B02B5C1E7AE37F2648DB5138315C27C57DC03C2C8F1DDB4D1C4C4ED012EB91780A931CEB5ADD9AB12431FDCB17600A0438DF03A8B7DED43E06DE466720C18EBDB503A61AB574B29BD7D522F4E95487DB254ABE754A48DB43AFEA176ABB37CEBE474BABC2A7CB84B8F0938BAB95C45BB6DA154D17ED25501870B97AB505D2694AAD42CBA2AD50856A6AA855374A556E1ABAE42213E92A92EB01AAC062AF1FA2857215E7FD011AF636898882BE71F4A2D4A0E5D75A6F8BB70BD588D86BACCB5C89B284A5572065D7DA638D36AD79A95B8A66BCD377DD7C018E9602D88E0A41C688D54C1B57364CD48AE3FE947301427583B72917AA4EFE0E8A5D7C8CF0E5575CB7DD42A5C24862F5C112836350B5E29597095FFA0466EF517ADD8EAB7D464FADA08C4B0C79D260631D13D4F5AB7E90D8A3EB4A06AEE753BF91C05A8A44330668D2398860726773175092DDA1A25921ED6654D517D29F7EED63609ECA9DE7747E3CCC435B24E1AA86BB0530D21C83D7820A63F2AE4DACFA5221D070CB836BA917F56C0C0A6F888B849700CDD6F9D14B0AE03AE0CE21A4E9AE78B31B0495DA7A1A65377E57912D669FC180839FDE27A217F1A980DCAF1B09601F821B2BBF1814E7F84EE36A91EBB4B913AE5C4732F52EF8D0DCAC11CC607FC14AF4B17F003121D058F6A4F7B7A44E0C3DEB47E9FDD070E3A34DDA71C89608722F29CAEFD8230435B1C2AE9D07D6C175EC303F2A63DB86DCF1B422E196101B4ACD23FF4BD0796C02382B4A7EC6148EC8B05CA5BCECDB7F9AC5C2F5609ECA7F266F37CF6719B147730CB5F2FC22CBA6949CC19CD245C08FBA74D9ED7C9F5BADEC8955A5467912E94BD655D5B06797096E6D175B0C8D9E745986551C1B8DD65B3D3E94B362958BE4EDE6FF3CD36675D66B62616222517DBC158FDF399D2E6F9FB4DF12BF3D105D6CCA8B8B6FA3E79BE8DE265D3EE579A5B700089629FB9BA535BC8322FEED6DEDC3794DEAD1322A18A7DCDF678F15248CC8865EF938BE04B08B7CDCC439163F31751709306ABACA2D196673F19FC96ABBB1FFE0F18ABD03944FF0000, '5.0.0.net40')
