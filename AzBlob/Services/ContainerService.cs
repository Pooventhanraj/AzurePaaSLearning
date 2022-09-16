using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzBlob.Services
{
    public class ContainerService : IContainerService
    {
        private readonly BlobServiceClient _blobClient;

        public ContainerService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }
        public async Task CreateContainer(string containerName)
        {
            var blobConatinerClient = _blobClient.GetBlobContainerClient(containerName);
            await blobConatinerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);

        }

        public async Task DeleteContainer(string containerName)
        {
            var blobConatinerClient = _blobClient.GetBlobContainerClient(containerName);
            await blobConatinerClient.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllContainer()
        {
            List<string> containerNames = new List<string>();

            await foreach (var item in _blobClient.GetBlobContainersAsync())
            {
                containerNames.Add(item.Name);

            }
            return containerNames;

        }

        public async Task<List<string>> GetAllContainerAndBlobs()
        {
            List<string> containerAndBlobNames = new();
            containerAndBlobNames.Add("Account Name : " + _blobClient.AccountName);
            containerAndBlobNames.Add("==========================================================================================================");
            containerAndBlobNames.Add("=========================================================================================================");
            await foreach (BlobContainerItem blobContainerItem in _blobClient.GetBlobContainersAsync())
            {
                containerAndBlobNames.Add("--" + blobContainerItem.Name);
                BlobContainerClient _blobContainer =
                      _blobClient.GetBlobContainerClient(blobContainerItem.Name);
                await foreach (BlobItem blobItem in _blobContainer.GetBlobsAsync())
                {
                    //get metadata
                    var blobClient = _blobContainer.GetBlobClient(blobItem.Name);
                    BlobProperties blobProperties = await blobClient.GetPropertiesAsync();
                    string blobToAdd = blobItem.Name;
                    if (blobProperties.Metadata.ContainsKey("title"))
                    {
                        blobToAdd += "(" + blobProperties.Metadata["title"] + ")";
                    }

                    containerAndBlobNames.Add("===================" + blobToAdd);
                }
                containerAndBlobNames.Add("=========================================================================================================");
                containerAndBlobNames.Add("=========================================================================================================");

            }
            return containerAndBlobNames;
        }
    }
}
