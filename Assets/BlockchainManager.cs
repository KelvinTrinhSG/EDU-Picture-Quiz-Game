using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using TMPro;
using UnityEngine.UI;

public class BlockchainManager : MonoBehaviour
{
    public string Address { get; private set; }

    public Button nftBtn;
    public Text nftBtnText;
    public Button playBtn;
    public Text playBtnText;
    public Button aboutBtn;
    public Button claimGoldBtn;
    public Text claimGoldBtnText;
    public Button moreGamesBtn;
    public Button quitBtn;

    public GameObject optionsBtn;
    public GameObject buttons;

    private string nftContractAddress = "0xaB51FfeCD2DA7d59b573C633163f12cf22285a8a";
    private string goldContractAddress = "0xe7B689acD8c08D75200c6C84B1e36e0473ff2EB3";

    private void Start()
    {
        nftBtn.gameObject.SetActive(false);
        playBtn.gameObject.SetActive(false);
        aboutBtn.gameObject.SetActive(false);
        claimGoldBtn.gameObject.SetActive(false);
        optionsBtn.SetActive(false);
        buttons.SetActive(false);
        moreGamesBtn.gameObject.SetActive(false);
        quitBtn.gameObject.SetActive(false);
    }

    public async void Login()
    {

        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        Contract contract = ThirdwebManager.Instance.SDK.GetContract(nftContractAddress);
        List<NFT> nftList = await contract.ERC721.GetOwned(Address);
        if (nftList.Count == 0)
        {
            nftBtn.gameObject.SetActive(true);
            playBtn.gameObject.SetActive(false);
            aboutBtn.gameObject.SetActive(false);
            claimGoldBtn.gameObject.SetActive(false);
            optionsBtn.SetActive(false);
            buttons.SetActive(false);
        }
        else
        {
            nftBtn.gameObject.SetActive(false);
            playBtn.gameObject.SetActive(true);
            aboutBtn.gameObject.SetActive(false);
            claimGoldBtn.gameObject.SetActive(false);
            optionsBtn.SetActive(false);
            buttons.SetActive(true);
        }
    }

    public async void ClaimNFTPass()
    {
        nftBtnText.text = "Claiming...";
        nftBtn.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract(nftContractAddress);
        var result = await contract.ERC721.ClaimTo(Address, 1);
        nftBtnText.text = "Claim Pass!";
        nftBtn.gameObject.SetActive(false);

        playBtn.gameObject.SetActive(true);
        aboutBtn.gameObject.SetActive(false);
        claimGoldBtn.gameObject.SetActive(false);
        optionsBtn.SetActive(false);
        buttons.SetActive(true);
    }

    public async void ClaimGold()
    {
        claimGoldBtn.interactable = false;
        claimGoldBtnText.text = "Claiming!";

        playBtn.interactable = false;
        aboutBtn.interactable = false;

        var contract = ThirdwebManager.Instance.SDK.GetContract(goldContractAddress);
        var result = await contract.ERC20.Claim("1");

        // Get the Player script component attached to the Player GameObject
        GameController.Instance.StartCoins = 100;

        Debug.Log("Token claimed");

        claimGoldBtnText.text = "+100 Gold";


        playBtn.interactable = true;
        aboutBtn.interactable = true;
    }
}
