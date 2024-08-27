using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using TMPro;
using UnityEngine.UI;

public class GoldBlockchainManager : MonoBehaviour
{
    public string Address { get; private set; }
    public Button goldClaimBtn;
    public TextMeshProUGUI goldClaimBtnText;

    private string goldContractAddress = "0xe7B689acD8c08D75200c6C84B1e36e0473ff2EB3";

    public async void ClaimGold()
    {
        goldClaimBtn.interactable = false;
        goldClaimBtnText.text = "Claiming!";

        var contract = ThirdwebManager.Instance.SDK.GetContract(goldContractAddress);
        var result = await contract.ERC20.Claim("1");

        // Get the Player script component attached to the Player GameObject
        GameController.Instance.EarnCoins(100, true);

        Debug.Log("Token claimed");

        goldClaimBtnText.text = "+100 Gold";
        goldClaimBtn.interactable = true;
    }
}
