using System.Collections; 
using UnityEngine; 
using UnityEngine.Networking; 
using UnityEngine.UI;
using System;
using System.Net;
using System.IO;

using UnityGLTF;
using UnityGLTF.Cache;
using UnityGLTF.Loader;

public class AssetLoader : MonoBehaviour{
    
    [SerializeField] Text assetCount;
    [SerializeField] Text loadedCount;
    int loaded=0;

    string baseUrl="https://content-assets-as-bundle.decentraland.org/v22"; 

    string[] assets=new string[]{ 
        "bafkreiawheluxd57zx3w5ha2buqrriyreavl4ekfllsfo7dte7tzacusui",
        "bafkreihxhvt3kql4ewcsl3j5na5uvctylwizp34hrzlodvovnwowsxbpfq",
        "bafkreieukg4dk2dl42vf52f5dkoefpebgn74i5rgcwy2ulml55uhovksbi",
        "bafybeid6udp5td7bxoicibs4daifrg5uirrkyb2aow63h26xnng4g3qzla",
        "bafkreieyeoybturbfbjrjhkmdddi4yjidahol2reencvrerxb53o3dudaq",
        "bafkreidvb2v64spxo5twbkxhkameiavqjpa5obsmz5koxktiwu2dweofza",
        "bafkreihoy3qxhyvdkvghi445kjldniosw6b7tuznp5mpkyjp3xw3zc3z64",
        "bafybeid45ggqqvk75hbr7q344kbfyzpczvsgjvwoc35omskc5sk6vfoowe",
        "bafybeicywotfshanwsw46d4toetlg26pcqwyvdvss6ufmvrm3xovon2pom",
        "bafybeia7f7q3wofeyimrcl7p3hloruqgmule2o2plvdgbugqhdinwfgdh4",
        "bafkreiggfqmbotprtjox3ryin2hisdqthnyma3xvvzb37z2xsgetern2cm",
        "bafkreiawxrs5b6fhgvd2mh7l5obpukd6g4zww2pl6aordcsc6n5nca3nwi",
        "bafkreidgir6x77p3bdi6guqhzsauzx7v6egs355gaaa3m4p4zjy73la4si",
        "bafkreif5iv7psuwawbaazzpjsziegp3p6pves2l73iauzfg5szgabou2zq",
        "bafkreihdlbaokzjmgm5k76tykv2aw3cujmikvspkbkcqrs7ai4mlkmjymy",
        "bafkreib66ufmbowp4ee2u3kdu6t52kouie7kd7tfrlv3l5kejz6yjcaq5i",
        "bafkreidxdhxc3eysjplo5hgfpdhcubk7ryvfflzfbvspwubpyem4auwjya",
        "bafkreifn7sjjlbkmdzxopyqr3cnwn6lbvhky5mmmqdmepppwcv3226wuvu",
        "bafkreifcc6ozydqyd26dfkro2s4mbmia35wmgnukxpfdrrzba2syrk7xje",
        "bafkreiehx62r5tzngkq2lhm5xmj6mx73t6om56plmlocvd6mdpuomjegxi",
        "bafkreidxonccuxoguapqfi3bjtb7sxyyshd5jfa5yc3whgk3qzpvxsrdoq",
        "bafkreie3s3h3cljuxtblcc3qg5xrkxbeumkvhkw7r735jnjoci652nge4m",
        "bafkreigvzfs3hnh2luhun6ysp5w7hrg6hfhpxt5hj3udpo6pr5lrwmeryi",
        "bafkreigzrfzyvtvgebn4gk2cko2l7j7wci3kv2nw5dh7sjrm5vmfavslx4",
        "bafkreic5ondc2z7wktcsf6mgawssy2jzn7rbdr564vji5xovvv5fjh4rgm",
        "bafybeiczp475ek2nptzi77ca4qd2in3o46istm5wl7ljj2dquxe2lf5wee",
        "bafybeidtlweu4mmq3soulecfwiujdr4tdsl3s555oonvzblbzoiyto32de",
        "bafybeic726zwyry45ghw6agom2ym5elkf2mgana6zai2hxyktut4w54a6q",
        "bafybeidfhp67b5xnwn6imzzy7eqog53huowh5er2fjshshqxpmhtqolf4u",
        "bafybeig2caysedrp6q6pjgdztrm4griozvdiaqkdijlxtkjxdu7ivk3s34",
        "bafybeid75asf75bb7llntzawqrvbs3v3uhmhwfzsw6nktvukkk2ydy5gua",
        "bafybeicn44hrbehzi2lzc2fllcpylifcibjhtqcxgiulgdg4gulvs3uqwq",
        "bafkreic7w6db5wyki5vdv2aasmnq4rqrr2nzu2hofuroosg2tsfcgs5kge",
        "bafkreigrv6k76ngist7yiszfllmi5dooectdhf4mzu7gxfrfmi4vmka5p4",
        "bafybeigil5yanbnp6rr4dykwotdyxbwcorn2iorzsoq474iuo4762sld2a",
        "bafybeihfgc6ola5tntzzh3hsjxvmpaoshupty4cf4ke3hajqdq5coxnixa",
        "bafkreib54zao7oxnsdodxn5ym75g2ns4kk4wr36mheadukb5yryiszlowy",
        "bafkreiaaf5xoqluy44m22n6ihnli36olrcjxenvuszemtuysiu7iw7dwly",
        "bafkreidtfxipuwb26e4xzmo4augnnc7jniwrb7gy7s6bnvkx6x3zk72dwu",
        "bafybeiewh2yrktcn6vhz2k3mfkpv6g7rh6kgy6ok5wan2z5tcmwtwsomda",
        "bafybeialk6m4lqibmvpfcgton2yuapua3wgpzkpf2icr4yx63z7zo3jdqu",
        "bafkreifls7hh5r2hknaeatogwlemumxhik5vyafpbyc7mkujcgi5kfnif4",
        "bafybeibntcdpnelz2qqkak3rq6kg5vnnt7qjlo7tuoszomm4npknqmf72m",
        "bafybeicxjbpolqefouen5owm7cwmdfqgzxzqqvr7nymixysdabtrzipzom",
        "bafybeibltjt62uzafdzktnt76zs6itphmbri3vrml3fatw46ypxw4fjycq",
        "bafkreiay2ppidmg4l72du4hqkcuzxyyfja5mlbad5quzw6vr64rp5xmnra",
        "bafkreicre4sx72ys2glhg2zqtwaqg4rw2pqx47p4agf3qoe2nhiz2csfh4",
        "bafybeiflswxc7xlfbnm4v2nrmqdt2ofm5qt752vku7ca344rjr3gn4bmfy",
        "bafybeib3plrf7hepyi7mpua64umt4esbzyappeahlfq632tl2t6ahsh6mi",
        "bafkreigi7b2laewxhdnf5yfzmdhrdxqghi2xaopeeb3d4wq3b2ij56lrv4",
        "bafkreib4apwmid35dakmrnam76nijfqd6bxttjm4667vwlslzfadv7tlby",
        "bafkreigmpihex63f2k5dbfnppvz76ntscaqfwuviof74i462tvyo6pqb3e",
        "bafybeihn6k6ixca7kpa6nw4euwotm2wnsh7ot5td5mbtj27leuyorxyg54",
        "bafkreidwmjj7tzpvy2e237ulyaeiynb5oyuxgntnazceuxgv3pn7ljpqy4",
        "bafkreiesx3qzzbmj6usw7g3dhjhqed7n4okz74bae4jvhhiioxjasosmm4",
        "bafkreidgtw52es5o44fejl446oc6zafnsly6pkxnpx53y5ob2dhssodm2m",
        "bafkreig7z6jotdaydao6sk3iugh6yj4uihnn6mqnbvefnkzu2jhjdgldna",
        "bafkreihr6xewnieokwiqanhawf7fye74pijq7f27wiruhurc6ldirfcdmi",
        "bafkreidann3f4pyo3muooax2peocbkjrmkrqtmdpv5a4dwytetvb3dedjy",
        "bafkreiba3f72nujx7ltchjpbcba62ii6ovzqvr7jvwo7ay7adq7hmms6rq",
        "bafkreih32xewt5hd2szfiudsbm2qf2tmigii6a3hko2cahfdhtdv3smquy",
        "bafybeiffndpibkq5rkwwh5zd26hlwrj3wsaiompjasp2i7phe23lomjj2u",
        "bafkreifcu76nnhz7pyov3narbqzwokt7wtiio3ki3wcx5bcvw6d3dneoje",
        "bafkreifpjov24j6otq6ifauhexnikochyzvosyi7fjgiwytzexnnih7nbe",
        "bafkreia3vndh2ezwvda4zwlcuuslv2nuv45cbvtou7l6x4fjjyb5jybm54",
        "bafkreidxobp4ctzfqhhdv5jk3bk4ltxzmzsgiy44jej7o3i2txr3ia33x4",
        "bafkreiffulqkz5l5lotvecf3qpwjj2e47g7lkug7afuyxff44dyhvji2yy",
        "bafkreihhjxgjj2tlpevgzamgy2qmymuq7awueclvfugeluaaepnpqoeppi",
        "bafkreiax3knlzjnnrxv5cbmeywuojpxumvr4zgh5c47l2lnghmgjsq64h4",
        "bafkreihhseedbllvxinfjonv654gkimgfw4reaalbgvytym5vofjbrv5gi",
        "bafkreiefdo2elxm24odxragmacdtuzdvxjtadbdr5qiqvbu2flzymc47eu",
        "bafkreickge73xskcw5xsumrbvg3gadstquanq6lojngtjrwtdp7bzjq32m",
        "bafkreiej5pemfao5fdj6doqme37e37246vbt5k277dstyu4ekwibx5n3la",
        "bafkreihhseedbllvxinfjonv654gkimgfw4reaalbgvytym5vofjbrv5gi",
        "bafkreiefdo2elxm24odxragmacdtuzdvxjtadbdr5qiqvbu2flzymc47eu",
        "bafkreickge73xskcw5xsumrbvg3gadstquanq6lojngtjrwtdp7bzjq32m",
        "bafkreiej5pemfao5fdj6doqme37e37246vbt5k277dstyu4ekwibx5n3la",
        "bafkreifg4g5dauljeglre3ojlyqpho6zxwwgckia5tfwxdqwrtzqiqhp3i",
        "bafkreigfygkxitstc4mbfdxlcjnsraf7ashrqwpw7h3s4cofictn2tvtby",
        "bafkreig4vlvhmri4gr5br7gfn7yn3cgt2gih5x6jttzsijxsfyfg42aueu",
        "bafkreia6uojd7bvy22z6wbewvboqxctq6iy4egddioysox6rhenalym7gm",
        "bafkreigksie6watz4qipapqwgizmyk33wpaxahzr5tovwtyzoeclkaynxm",
        "bafkreibs45niza5qo3rfook5b4vvknfsfni3m3gnvlizaqhl72h2n2p44q",
        "bafkreihamlpaa7p7zwmoojuj2iiq7yad53ajt3m36hmz2g6piq5xtuadqq",
        "bafkreie3hweovfni7ffseay6uxzxytt3g5ton3ivqke6wsxcaniu6z7t6u",
        "bafkreiho5vo6634uht4bbxe7r3a44sqsrs2tarshmcr7l35lupx5an46oq",
        "bafkreifovlimgmbaifhcmykhlk5cfe7sbjgducc2fjzdsmuyxxya3vxay4",
        "bafkreieakrpobrorsa3qip7zudmngo74acctde76lygarqgiwlg6crlo3u",
        "bafkreif25hqacbzev25iii7ldturl5vgbgryuvrtxysnb4myxq44rbwioa",
        "bafkreif25hqacbzev25iii7ldturl5vgbgryuvrtxysnb4myxq44rbwioa",
        "bafkreifh7g6mpa5dgifmqo42ijdo4ediaic2uvmcvrte6t2uyehus7hwce",
        "bafkreiabfv2qjptxb5iaizdn6nnuhhovxwbjqxsbl7ld2fto32wqh6mdua",
        "bafkreiabfv2qjptxb5iaizdn6nnuhhovxwbjqxsbl7ld2fto32wqh6mdua",
        "bafybeibgpkdsthygnoqj4t4aqyy7mtobqxqwlceish7gkqvxogiin7dpmm",
        "bafkreie5pqjly3m2bxwq5gy7kbyo2v2oeicc5mcbqpib7rpikkwexsife4",
        "bafkreid7fth6hdt5fate5jruog75g4tr2uz3jfq2inyhbp2th5gnmbrk6e",
        "bafybeifu3kcx3xod64i5aecamxa73ripjs462ydspd6wm6d2f345veecvm",
        "QmZP52KMWEUcnwVG68kNYmof8oMRM6z2PC3QA5enHb8U3S",
        "bafkreid2aswbhebm5lwcjtsuwrrpz2jczgwvwavbelb22n7myftlhcpa4q",
        "bafkreifbmurixopnslku6i2xqizxz5altgi3ogo6vzysjqpel7av2sze54",
        "QmU12QmJmU2bwGaqoZF9MarxeVKciRiPo8tHi7YArNf4D7",
        "QmTUHuH5qn9SJV1Ch4eax1jbMKa5QWLERQTUq7hVE5vNWF",
        "QmX6NmvbLJv2CiXAQy2ynXMHEhy8bR5suJNr3gXYcMBRpg",
        "QmNVyJLriAogKsaa6yUygy7F74Rs1ZGMAvm7WkiQGTddWp",
        "QmYRroG5YvELQiWWboBzDkDSEopp7MiampTmYf5iREWuSe",
        "bafkreieyeoybturbfbjrjhkmdddi4yjidahol2reencvrerxb53o3dudaq",
        "bafybeibe4grmw5o5vkemz7gqy4lgfbchan5orlu34l36o6fct6de73jrum",
        "bafybeib2dyouwalssjq4drlecakb75hzrfslocsfnxugpzxf6umvfr7ybm",
        "bafybeicnic3exs4vosi72ldpnvlb3oadfbtvau5w37mvt3cro4ucj6yjaa",
        "bafybeihwyi5nskxsfm7xemgjkp53vvyjkdzag2ogkpaupop6nvhe72dvzq",
        "bafkreiaxs7bir7dl3isevljzgtqs2igt5c4vag7dmsztryengflvlsnzhu",
        "bafkreibxg6iey66ozqgunuxp47eikdkqeqvv6kcfdj23m3baoboladluri",
        "bafkreid3qqyprvtyg5sntwwxisgdzmavivjonyzph4zx5s6ri66nh3iqta",
        "bafkreid54cwdezzi5j57j7uxtheol2prt3zqermzeomxzcieqabz4eefyi",
        "bafkreid5l6vp3lkie5ttk4f3ulv7dowdqqhejtdpl4dfeoxpmxy2pwyr6y",
        "bafkreidf4jvxqbpu4fbm7qrnemyh2cv5ueen3vnneg6ghtriklqxk75nqi",
        "bafkreidpcgsz56uu6q52zi4oiyxau7hu6r5bpjux2o6ornkyy6xx2kmkry",
        "bafkreieiiaz2dbavk3gyiq3v37f6kcxvmpba6yckg7ireifvvgrd75nthu",
        "bafkreih62x47l43zpispvntdxgf7l6ylmlfgehff53vmbpb6qvmo3nebbi",
        "bafkreihkw7yvmbmc22vlfty7uyyswoosu4dj64ajquxo7wgdleb4ycwczu",
        "bafkreihntcm4ozbq4frrrlafz2zflc7aasnpjz2twbfvirnmmqdir2qh6u",
        "bafkreihutf5stobagptymwgbjp3tqba3xspubuyhvnkmuehwstj6od3lg4",
        "bafkreihzjki5i33hdjeyosx3dox3us63227orcmvzxftxpohx4zrd5g6pu",
        "bafybeigiy4ftebu3dcs7ssj373t2ifz43vpabqf5fi2fv3xe6pvo7ohgoi",
        "bafkreiasmh4qn4cilx2yo5xlxxiw4f7m5ulmg6ifw6i3pkty5lja3h5eua",
        "bafkreid2lkupz4ptvl73wy46pa7ksequ7vkvpdvlx72vespoukqerg3nqu",
        "bafkreiezenrkatc3fhnw7teksszrnpagz773635ghr5gil2xjxmzdjm2jm",
        "bafkreihhhzllwwnvaea7lxi37l2qanxgmhemdhkuqcdqequyowlmhoe2um",
        "bafybeicnpx6btibtkejrfsfjfy75moxnxubj7rwfkszdc6qo5zfp6i5ham",
        "bafkreiav4pp7ixhbztvekarca6ardlxnxy766sz4je6g57kcvxlnuq5d74",
        "bafkreibfutn7mfd2mu3ux6g5eg6qek3gctuhdcot2y4mjzttwzmiqrwlpi",
        "bafkreibgytmtd5wyd4qfbkpyte35zefwvl5mu7m3qpu5g3zoyzhpj3pary",
        "bafkreids5aumbhor3pshykkrratuozxchvrz7liuraxvtd5eskfb56oj7y",
        "bafkreigpzkynaxzqhgu5ibq5w3dqeyhm43yvgebibce233zqy4t4qzcna4",
        "bafkreigr4zwzplnecvv67edzsid63z5fs4g54ifbmyqcvkfiwdpv3exie4",
        "bafkreifqyef4eaerv6gwlwc5c35lkzjy3s4g7s2zuzvi6kjfb7vedvnl74",
        "QmZP52KMWEUcnwVG68kNYmof8oMRM6z2PC3QA5enHb8U3S",
        "Qme6Dg9gtJUrVCNe4oVxF9gHezCMp6R5FrtdVKquDyow9e",
        "QmPeLCT8Vzc67ECmQyiSEoXRz54EQBfikQHif6xudsbpz5",
        "QmRn7CB7CTQkMBgoCzRfghQnjpdz9x6w1saxiWPeuvxb2o",
        "QmZkHSwq2TeTLAPEfqQhxT7FFtMUMcX9FZTkgUbfPGD2ht",
        "QmU12QmJmU2bwGaqoZF9MarxeVKciRiPo8tHi7YArNf4D7",
        "QmTUHuH5qn9SJV1Ch4eax1jbMKa5QWLERQTUq7hVE5vNWF",
        "QmX6NmvbLJv2CiXAQy2ynXMHEhy8bR5suJNr3gXYcMBRpg",
        "QmNVyJLriAogKsaa6yUygy7F74Rs1ZGMAvm7WkiQGTddWp",
        "QmYRroG5YvELQiWWboBzDkDSEopp7MiampTmYf5iREWuSe",
        "QmXZnxX4hbKjCRJkLmVrPzGv876aUzY8dGxmVGngThnX7t",
        "QmYktkLr5rnn9zPPARkavhVowvTNTih8uWq8BVscTGxtZD"
    }; 

 

    string localRoot {get=>$"{Application.dataPath}/../Logs/ABEY/Assets";} 
    string[] localAssets = new string[]{   }; 

 

 

    IEnumerator Start() { 
        assetCount.text = assets.Length.ToString();
        LocalFiles();
        yield return null;
       //yield return RemoteFiles();  

    } 


    void LocalFiles(){ 
        //Hardcoded to read the Brown building's asset bundle
        AssetBundle bundle = AssetBundle.LoadFromFile($"{localRoot}/bafybeib3plrf7hepyi7mpua64umt4esbzyappeahlfq632tl2t6ahsh6mi"); 
       // yield return bundle; // 'waits for the bundle to be loaded

        // DEBUG create a list of all the assets in this bundle so we can print it out
        string assets="";
         foreach(string file_path_in_bundle in bundle.GetAllAssetNames()){ 
           assets+=file_path_in_bundle+"\n"; 
        }

         
                UnityEngine.Object[] assetsObjs = bundle.LoadAllAssets();

                foreach (UnityEngine.Object asset in assetsObjs)
                {
                    if (asset is Material material){
                        material.shader = Shader.Find("DCL/Universal Render Pipeline/Lit");
                    }

                    if (asset is GameObject assetAsGameObject){
                        GameObject instance = Instantiate(assetAsGameObject, new Vector3(233f,0f,137f), Quaternion.identity);

                        // look up this to see what the patch does
                      //  PatchSkeletonlessSkinnedMeshRenderer(instance.GetComponentInChildren<SkinnedMeshRenderer>());

                     //   results.Add(instance);
                        instance.name = instance.name.Replace("(Clone)", "");
                    }
                }


        // try to load one of the assets in the bundle

      //  GameObject loadAsset = bundle.LoadAsset<GameObject>("bafybeib3plrf7hepyi7mpua64umt4esbzyappeahlfq632tl2t6ahsh6mi");
        //yield return loadAsset;
      //  Debug.Log(loadAsset);
         //if(loadAsset!=null){
      //      Instantiate(loadAsset, Vector3.zero, Quaternion.identity); 
      //  }
        //'stream' being your filestream/memorystream
      //  UnityGLTF.Loader.ILoader loader = new UnityGLTF.Loader.WebRequestLoader(URIHelper.GetDirectoryName($"assets/_downloaded/bafybeib3plrf7hepyi7mpua64umt4esbzyappeahlfq632tl2t6ahsh6mi.glb"));
			
			//yield return sceneImporter.LoadScene(null, -1, true);
			

        //DEBUG print to the log The list of assets we created
        Debug.Log(assets);
        /* it Prints
            assets/_downloaded/bafybeib3plrf7hepyi7mpua64umt4esbzyappeahlfq632tl2t6ahsh6mi/bafybeib3plrf7hepyi7mpua64umt4esbzyappeahlfq632tl2t6ahsh6mi.glb <- the 3D model ".glb is not supported by unity, has to be converted"
            assets/_downloaded/bafybeib3plrf7hepyi7mpua64umt4esbzyappeahlfq632tl2t6ahsh6mi/materials/atlas cristal.mat    <- a material for a mesh on the 3D model
            assets/_downloaded/bafybeib3plrf7hepyi7mpua64umt4esbzyappeahlfq632tl2t6ahsh6mi/materials/atlas light.mat      <- a material for a mesh on the 3D model
            assets/_downloaded/bafybeib3plrf7hepyi7mpua64umt4esbzyappeahlfq632tl2t6ahsh6mi/materials/atlas metal.mat      <- a material for a mesh on the 3D model
            assets/_downloaded/bafybeib3plrf7hepyi7mpua64umt4esbzyappeahlfq632tl2t6ahsh6mi/materials/atlas solid.mat      <- a material for a mesh on the 3D model
            assets/_downloaded/bafybeib3plrf7hepyi7mpua64umt4esbzyappeahlfq632tl2t6ahsh6mi/materials/floor_concrete.mat   <- a material for a mesh on the 3D model
            assets/_downloaded/bafybeib3plrf7hepyi7mpua64umt4esbzyappeahlfq632tl2t6ahsh6mi/materials/floor_wood.mat       <- a material for a mesh on the 3D model
            assets/_downloaded/bafybeib3plrf7hepyi7mpua64umt4esbzyappeahlfq632tl2t6ahsh6mi/materials/led_tx.mat           <- a material for a mesh on the 3D model
            assets/_downloaded/bafybeib3plrf7hepyi7mpua64umt4esbzyappeahlfq632tl2t6ahsh6mi/metadata.json                  <- DCL data about this bundle

            My use with assetbundles, you only need to clone the 3D object and not worry avout the .mat files, the cloning of the model should load them automaitically for you
            DCL how ever is using .gl vs unity's supported .fbx (its .(gl)b and (gl)tf -> b=Binary and tf=Text Format)
            So I need to find how its being converted into a usable .fbx we can clone into the scene
        */



       
         /*{
             "timestamp":637820366614503300,
             "version":"2.0","dependencies":[
                 "bafkreiatc7mar3txvsmu5i63watlnqreuusdto3houyhge4iap4xfdbtxm","bafkreid2aswbhebm5lwcjtsuwrrpz2jczgwvwavbelb22n7myftlhcpa4q","bafkreidrxz56plgo6mzk7hmb7gvzzqpifvv6eueq7sewfpeal5sjncih7i",
                 "bafkreifvnkt2ts5dwtxy3ch7avlznlyovbd7vrtaco67fordut5ztuzkwe","bafkreihzxzuysemzjlednm3zjx3ogtzphry5r2joygpkr5mdmvuwgrjsky","mainshader_delete_me"
                 ]
            }*/

        
        /*

        foreach(string hash in assets){ 
            AssetBundle bundle = AssetBundle.LoadFromFile($"{localRoot}/{hash}"); 
            yield return bundle; 
            string loadedassets = ""; 
            Debug.Log(bundle); 
            //foreach(string file_path_in_bundle in bundle.GetAllAssetNames()){ 
                //loadedassets += $"{file_path_in_bundle}\n";


        var loadAsset = bundle.LoadAssetAsync<GameObject>($"assets/_downloaded/{hash}.glb"); 

        yield return loadAsset; 

        if(loadAsset.asset!=null){
            Instantiate(loadAsset.asset); 
        }
    

          //  } 

            Debug.Log(loadedassets); // just spits out the assets path within the Asset Bundle 

        } 
*/
    } 

    IEnumerator RemoteFiles(){ 

        foreach(string hash in assets){ 

            //UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle($"{baseUrl}/{hash}");
            UnityWebRequest uwr = UnityWebRequest.Get($"{baseUrl}/{hash}"); 
            yield return uwr.SendWebRequest(); 


            if (uwr.result != UnityWebRequest.Result.Success) {
                Debug.Log(uwr.error);
                ABEY.LogWriter.Write(hash, uwr.error, 30);
            } else {

                File.WriteAllBytes($"{Application.dataPath}/../Logs/ABEY/Assets/{hash}", uwr.downloadHandler.data);
                 loaded++;
                loadedCount.text = loaded.ToString();
/*
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr); 
            
                
                Debug.Log(bundle); 
                loaded++;
                loadedCount.text = loaded.ToString();
            
                foreach(string file_path_in_bundle in bundle.GetAllAssetNames()){ 
                   // ABEY.LogWriter.Write(hash, file_path_in_bundle, 30);
                    Debug.Log(file_path_in_bundle); 

                    /* 

                    This is how Unity would create the objects in worl, but you need to check the type of assets, this way is for gameobjects not textures or materials etc... 

                    var loadAsset = bundle.LoadAssetAsync<GameObject>(file_path_in_bundle); 

                    yield return loadAsset; 

                    Instantiate(loadAsset.asset); 

                     

                } 
                */

            } 

        } 
    }



}
