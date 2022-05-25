namespace ABEY {
    using UnityEngine;

    [CreateAssetMenu(fileName = "EndPointConfigScriptable", menuName = "ABEY/EndPointConfigScriptable", order = 0)]
    public class EndPointConfigScriptable : ScriptableObject {

        //look into this one later
            // Assets\Scripts\MainScripts\DCL\Helpers\Utils\TestAssetsUtils\TestAssetsUtils.cs
            //"http://127.0.0.1:9991" <- use webserver
            //Application.dataPath + "/../TestResources" <- use files note threre is sample AB, glbs etc...
            
            [SerializeField] public string mapApiBaseUrl = "http://debugmode.online:5000/v1/map.png";


            
        [Header("Graph")]
    /**** -Graph   **/
            // Assets\Scripts\MainScripts\DCL\ServiceProviders\TheGraph\TheGraph.cs
            [SerializeField] public string landSubgraphUrlOrg                   = "https://api.thegraph.com/subgraphs/name/decentraland/land-manager";
            [SerializeField] public string landSubgraphUrlZone                  = "https://api.thegraph.com/subgraphs/name/decentraland/land-manager-ropsten";
            [SerializeField] public string landSubgraphUrlMatic                 = "https://api.thegraph.com/subgraphs/name/decentraland/mana-matic-mainnet";
            [SerializeField] public string nftCollectionsSubgraphUrlEthereum    = "https://api.thegraph.com/subgraphs/name/decentraland/collections-ethereum-mainnet";
            [SerializeField] public string nftCollectionsSubgraphUrlMatic       = "https://api.thegraph.com/subgraphs/name/decentraland/collections-matic-mainnet";
        [Header("Wearables")]
    /**** -Wearables   **/
            // Assets\Scripts\MainScripts\DCL\ServiceProviders\Wearables\WearablesAPIData.cs
            [SerializeField] public string wearablesContentBaseUrl   = "https://peer-lb.decentraland.org/content/contents/";
            // Assets\Scripts\MainScripts\DCL\ServiceProviders\Wearables\WearablesFetchingHelper.cs
            [SerializeField] public string wearablesFetchUrl         = "https://peer-lb.decentraland.org/lambdas/collections/wearables?";
            [SerializeField] public string baseWearablesCollectionId = "urn:decentraland:off-chain:base-avatars";
            [SerializeField] public string collectionsFetchUrl       = "https://nft-api.decentraland.org/v1/collections?sortBy=newest&first=1000";
        [Header("?")]
    /**** -RequestController  ? **/
            [SerializeField] public string editorUserAgent  = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
            [SerializeField] public string editorReferrer   = "https://play.decentraland.org"; 




            public string MapApiBaseUrl => mapApiBaseUrl;

            // Graph
            public string LandSubgraphUrlOrg                => landSubgraphUrlOrg;
            public string LandSubgraphUrlZone               => landSubgraphUrlZone;
            public string LandSubgraphUrlMatic              => landSubgraphUrlMatic;
            public string NftCollectionsSubgraphUrlEthereum => nftCollectionsSubgraphUrlEthereum;
            public string NftCollectionsSubgraphUrlMatic    => nftCollectionsSubgraphUrlMatic;
        
            // Wearables
            public string WearablesContentBaseUrl => wearablesContentBaseUrl;

            // RequestController  ?
            public string EditorUserAgent  => editorUserAgent;
            public string EditorReferrer   => editorReferrer;
        
    }
}