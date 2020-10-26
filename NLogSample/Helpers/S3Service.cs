﻿using Amazon;
using Amazon.CognitoIdentity;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NLogSample.Helpers
{
    public class S3Service
    {
        private const string bucketName = "aws-s3-theo-test";
        private const string keyName = "keyplus_log";
        //private const string filePath = "*** provide the full path name of the file to upload ***";
        // Specify your bucket region (an example region is shown).
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APNortheast2;
        private static IAmazonS3 s3Client;


        //public static void Main()
        //{
        //    s3Client = new AmazonS3Client(bucketRegion);
        //    UploadFileAsync().Wait();
        //}

        public static async void UploadFileAsync(string filePath)
        {
            // Amazon Cognito 인증 공급자를 초기화합니다
            CognitoAWSCredentials credentials = new CognitoAWSCredentials(
                "ap-northeast-2:8a926cbe-42e6-428f-92f0-6ac406b12604", // 자격 증명 풀 ID
                RegionEndpoint.APNortheast2 // 리전
            );

            s3Client = new AmazonS3Client(credentials, bucketRegion);

            try
            {
                var fileTransferUtility =
                    new TransferUtility(s3Client);

                // Option 1. Upload a file. The file name is used as the object key name.
                await fileTransferUtility.UploadAsync(filePath, bucketName);
                Console.WriteLine("Upload 1 completed");

                //    // Option 2. Specify object key name explicitly.
                //    await fileTransferUtility.UploadAsync(filePath, bucketName, keyName);
                //    Console.WriteLine("Upload 2 completed");

                //    // Option 3. Upload data from a type of System.IO.Stream.
                //    using (var fileToUpload =
                //        new FileStream(filePath, FileMode.Open, FileAccess.Read))
                //    {
                //        await fileTransferUtility.UploadAsync(fileToUpload,
                //                                   bucketName, keyName);
                //    }
                //    Console.WriteLine("Upload 3 completed");

                //    // Option 4. Specify advanced settings.
                //    var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                //    {
                //        BucketName = bucketName,
                //        FilePath = filePath,
                //        StorageClass = S3StorageClass.StandardInfrequentAccess,
                //        PartSize = 6291456, // 6 MB.
                //        Key = keyName,
                //        CannedACL = S3CannedACL.PublicRead
                //    };
                //    fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
                //    fileTransferUtilityRequest.Metadata.Add("param2", "Value2");

                //    await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
                //    Console.WriteLine("Upload 4 completed");
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }

        }
    }
}