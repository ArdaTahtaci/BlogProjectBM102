### How To Use

When first open the application, a **wallet connection** is initiated using the `Connect` button. If **no wallet** is found on the browser, a **warning message** is displayed. You need to select one of the detected **Ethereum wallets** to proceed.

After connecting your wallet, proceed by selecting a file to upload to IPFS. Once the file is chosen, click the **Upload to IPFS** button. After uploading, continue by pressing the **Encrypt Hash** button. At this stage, the wallet client will open, prompting the user to complete a signature request. This signature generates a key for encryption purposes.

The IPFS hash (address) of the uploaded file is then encrypted using **AES** with the generated key. Once the encryption is complete, the encrypted address is ready to be saved to the contract. When you press the **Save** button, the wallet client will reopen to request permission for interacting with the contract. Since the transactions occur on the **Ethereum Sepolia Testnet**, they may take **20–40 seconds** to process.

By pressing the **Get My Files** button, the application retrieves the user’s files. For decryption, another signing request is initiated to generate the decryption key. If the user wants to delete a file, the encrypted hash of the file is provided as a parameter, and the record of the address in the contract is removed by interacting with the wallet client.

### Uploading Files to IPFS with Pinata

To upload files to IPFS using Pinata, an API key was obtained from the account, and a Pinata SDK object was created:

```javascript
const pinata = new PinataSDK({
    pinataJwt: process.env.REACT_APP_PINATA_JWT,
    pinataGateway: "gateway.pinata.cloud",  // Default Pinata Gateway
});
```
Below is the function to upload a file to IPFS:

```javascript
const uploadToIPFS = async (file: File) => {
    try {
        const res = await pinata.upload.file(file);
        return res.IpfsHash;
    } catch (error: any) {
        console.log(error.message);
    }
    return "";
};
```
### Encryption and Decryption

### AES Encryption and Decryption Using CryptoJS

The following functions use the **CryptoJS** library to perform AES encryption and decryption.

```javascript
import CryptoJS from "crypto-js";

// AES Encryption function
const encryptData = (data: string, key: CryptoJS.lib.WordArray) => {
    return CryptoJS.AES.encrypt(data, key, {
        mode: CryptoJS.mode.ECB, // AES ECB modunu kullanıyoruz (block chaining için CBC kullanılabilir)
        padding: CryptoJS.pad.Pkcs7 // PKCS7 padding kullanımı
    }).toString();
};

// AES Decryption function
const decryptData = (encryptedData: string, key: CryptoJS.lib.WordArray) => {
    const decrypted = CryptoJS.AES.decrypt(encryptedData, key, {
        mode: CryptoJS.mode.ECB,
        padding: CryptoJS.pad.Pkcs7
    });
    return decrypted.toString(CryptoJS.enc.Utf8); // UTF-8 olarak geri çevir
};
```

### Smart Contract Operations

The project folder containing the contract was initialized using the following command:

```bash
npx hardhat init
```

In the `hardhat.config.js` file:

- **URL**: A JSON RPC URL was configured using an API key from the chosen node provider.
- **Networks**: The Sepolia Testnet was specified as the target network.
- **Accounts**: Wallet accounts were added to manage transactions during contract deployment.

The deployment script was written in the `scripts/deploy.js` file and deployed to the Ethereum Sepolia Testnet using the following command:

```bash
npx hardhat run scripts/deploy.js --network sepolia
```
This setup allows seamless deployment and interaction with the contract on the testnet environment.


### Interaction with the Contract on the Client

The **Ethers.js** library was used for client-side interactions with the contract. After sending a connection request with `request({ method: 'eth_requestAccounts' })`, the wallet provider and signer were obtained from the `ethereum` object within `window`. This setup enables relevant wallet interactions.

Using Ethers.js, a **contract object** was created. In the `constants` file, the address of the contract deployed on the Sepolia network and its ABI are stored. This contract object is essential for sending requests.

The related functions can be found in the `src/context` directory, specifically within `HashContext` and `WalletContext`.
