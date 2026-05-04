using UnityEngine;

public class OfflicenseCustomerScript : MonoBehaviour
{
    [SerializeField] private OffLicenseCustomerRequests customerRequestData;

    public string alcoholType;
    public string flavourProfile;
    public string paleOrDark;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alcoholType = customerRequestData.alcoholType;
        flavourProfile = customerRequestData.flavourProfile;
        paleOrDark = customerRequestData.paleOrDark;
    }
}
