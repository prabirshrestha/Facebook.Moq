﻿// uncomment the following code to enable Moq helpers for Websites
// #define FACEBOOK_MOQ_WEB

namespace Facebook.Moq
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Facebook;
    using global::Moq;
    using global::Moq.Language.Flow;
    using global::Moq.Protected;

    public static class FacebookMock
    {
        public static Mock<FacebookClient> New()
        {
            return new Mock<FacebookClient> { CallBase = true };
        }

        #region Setup

        public static ISetup<FacebookClient, object> FbSetup(this Mock<FacebookClient> mock,
                                                             Expression path, Expression parameters,
                                                             Expression httpMethod, Expression type)
        {
            return mock.Protected()
                .Setup<object>("Api",
                               path ?? ItExpr.IsAny<string>(),
                               parameters ?? ItExpr.IsAny<IDictionary<string, object>>(),
                               httpMethod ?? ItExpr.IsAny<HttpMethod>(),
                               type ?? ItExpr.IsAny<Type>());
        }

        public static ISetup<FacebookClient, object> FbSetup(this Mock<FacebookClient> mock)
        {
            return mock.FbSetup(null, null, null, null);
        }

        public static ISetup<FacebookClient, object> FbSetupIfPath(this Mock<FacebookClient> mock, Expression<Func<string, bool>> match)
        {
            return mock.FbSetup(ItExpr.Is(match), null, null, null);
        }

        public static ISetup<FacebookClient, object> FbSetupIfPathIs(this Mock<FacebookClient> mock, string path)
        {
            return mock.FbSetupIfPath(p => p == path);
        }

        public static ISetup<FacebookClient, object> FbSetupIfParameter(this Mock<FacebookClient> mock, Expression<Func<IDictionary<string, object>, bool>> match)
        {
            return mock.FbSetup(ItExpr.IsAny<string>(), ItExpr.Is(match), null, null);
        }

        public static ISetup<FacebookClient, object> FbSetupIfRestMethodIs(this Mock<FacebookClient> mock, string method)
        {
            return mock.FbSetupIfParameter(p => p != null && p.ContainsKey("method") && method.Equals((string)p["method"], StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region ReturnsJson

        public static IReturnsResult<FacebookClient> ReturnsJson(this ISetup<FacebookClient, object> setup, object json)
        {
            return setup.Returns(json);
        }

        public static IReturnsResult<FacebookClient> ReturnsJson(this ISetup<FacebookClient, object> setup, string json)
        {
            return setup.ReturnsJson(JsonSerializer.Current.DeserializeObject(json));
        }

        public static IReturnsResult<FacebookClient> ReturnsJson<T>(this ISetup<FacebookClient, object> setup,
                                                                    string json)
        {
            return setup.ReturnsJson(JsonSerializer.Current.DeserializeObject<T>(json));
        }

        #endregion

        #region Callback

        public static IReturnsThrows<FacebookClient, object> FBCallback(this ISetup<FacebookClient, object> setup, Action<string, IDictionary<string, object>, HttpMethod, Type> callback)
        {
            return setup.Callback(callback);
        }

        public static ICallbackResult FbCallback(this IReturnsResult<FacebookClient> returnsResult, Action<string, IDictionary<string, object>, HttpMethod, Type> callback)
        {
            return returnsResult.Callback(callback);
        }

        public static ICallbackResult FbCallbackPath(this IReturnsResult<FacebookClient> returnsResult, Action<string> callback)
        {
            return returnsResult.FbCallback((path, parameters, method, type) => callback(path));
        }

        public static ICallbackResult FbCallbackParameters(this IReturnsResult<FacebookClient> returnsResult, Action<IDictionary<string, object>> callback)
        {
            return returnsResult.FbCallback((path, parameters, method, type) => callback(parameters));
        }

        public static ICallbackResult FbCallback(this IReturnsResult<FacebookClient> returnsResult, Action<string, IDictionary<string, object>> callback)
        {
            return returnsResult.FbCallback((path, parameters, method, type) => callback(path, parameters));
        }

        #endregion

        public static class Exceptions
        {
            public static FacebookOAuthException AccessTokenRequired()
            {
                return new FacebookOAuthException("An access token is required to request this resource.", "OAuthException");
            }

            public static FacebookOAuthException ActiveAccessTokenRequired()
            {
                return new FacebookOAuthException("An active access token must be used to query information about the current user.", "OAuthException");
            }

            public static FacebookOAuthException InvalidOAuthAccessToken()
            {
                return new FacebookOAuthException("Invalid OAuth access token.", "OAuthException");
            }

            public static FacebookOAuthException AliasRequestedDoesNotExist(string path)
            {
                return new FacebookOAuthException(string.Concat("(#803) Some of the aliases you requested do not exist: ", path), "OAuthException");
            }

            public static FacebookOAuthException RequiresAValidAppId()
            {
                return new FacebookOAuthException("(#200) This API call requires a valid app_id.");
            }

            public static FacebookOAuthException UnknownPathComponent(string path)
            {
                if (!path.StartsWith("/"))
                    path += "/";
                return new FacebookOAuthException(string.Concat("Unknown path components: ", path.Replace("/", "\\/")), "OAuthException");
            }
        }

        #region RestMethods

        public static class RestMethods
        {
            // ReSharper disable InconsistentNaming
            public const string ads_addAccountGroupAccounts = "ads.addAccountGroupAccounts";
            public const string ads_addAccountGroupUsers = "ads.addAccountGroupUsers";
            public const string ads_addAccountUsers = "ads.addAccountUsers";
            public const string ads_createAccountGroup = "ads.createAccountGroup";
            public const string ads_createAdGroups = "ads.createAdGroups";
            public const string ads_createAdreportSchedules = "ads.createAdreportSchedules";
            public const string ads_createCampaigns = "ads.createCampaigns";
            public const string ads_createCreatives = "ads.createCreatives";
            public const string ads_estimateTargetingStats = "ads.estimateTargetingStats";
            public const string ads_getAccountGroups = "ads.getAccountGroups";
            public const string ads_getAccounts = "ads.getAccounts";
            public const string ads_getAdGroupCreatives = "ads.getAdGroupCreatives";
            public const string ads_getAdGroupStats = "ads.getAdGroupStats";
            public const string ads_getAdGroupTargeting = "ads.getAdGroupTargeting";
            public const string ads_getAdGroups = "ads.getAdGroups";
            public const string ads_getAdreportJobs = "ads.getAdreportJobs";
            public const string ads_getAdreportSchedules = "ads.getAdreportSchedules";
            public const string ads_getAutoCompleteData = "ads.getAutoCompleteData";
            public const string ads_getCampaignStats = "ads.getCampaignStats";
            public const string ads_getCampaigns = "ads.getCampaigns";
            public const string ads_getConnectionObjectIds = "ads.getConnectionObjectIds";
            public const string ads_getCreativeIdsByAccount = "ads.getCreativeIdsByAccount";
            public const string ads_getCreatives = "ads.getCreatives";
            public const string ads_getKeywordAutocomplete = "ads.getKeywordAutocomplete";
            public const string ads_getKeywordSuggestions = "ads.getKeywordSuggestions";
            public const string ads_getValidKeywords = "ads.getValidKeywords";
            public const string ads_removeAccountGroupAccount = "ads.removeAccountGroupAccount";
            public const string ads_removeAccountGroupUsers = "ads.removeAccountGroupUsers";
            public const string ads_removeAccountUsers = "ads.removeAccountUsers";
            public const string ads_setAccountGroupUsersRole = "ads.setAccountGroupUsersRole";
            public const string ads_setAccountUsersRole = "ads.setAccountUsersRole";
            public const string ads_updateAccountGroup = "ads.updateAccountGroup";
            public const string ads_updateAdGroups = "ads.updateAdGroups";
            public const string ads_updateAdreportSchedules = "ads.updateAdreportSchedules";
            public const string ads_updateCampaigns = "ads.updateCampaigns";
            public const string ads_updateCreatives = "ads.updateCreatives";
            // ReSharper restore InconsistentNaming
        }

        #endregion

        public static class MediaObjects
        {
            public static FacebookMediaObject DummyMediaObject(string contentType, string fileName)
            {
                return new FacebookMediaObject
                           {
                               ContentType = contentType,
                               FileName = fileName
                           }.SetValue(new byte[] { 0, 0, 0 });
            }

            public static FacebookMediaObject DummyImageMediaObject()
            {
                return new FacebookMediaObject
                {
                    ContentType = "image/jpeg",
                    FileName = "dummy.img"
                }.SetValue(new byte[] { 0, 0, 0 });
            }

            public static FacebookMediaObject DummyVideoMediaObject()
            {
                return new FacebookMediaObject
                {
                    ContentType = "video/3gpp",
                    FileName = "dummy.3gp"
                }.SetValue(new byte[] { 0, 0, 0 });
            }

            public static FacebookMediaObject MediaObjectImageJpgeg()
            {
                return new FacebookMediaObject { ContentType = "image/jpeg", FileName = "fbapi.jpeg" }.SetValue(ImageJpeg);
            }

#if FACEBOOK_MOQ_WEB

            public static Mock<System.Web.HttpPostedFileBase> ToHttpPostedFileBase(FacebookMediaObject mediaObject)
            {
                var mock = new Mock<System.Web.HttpPostedFileBase>();

                var data = mediaObject.GetValue();

                mock.Setup(m => m.ContentLength).Returns(data.Length);
                mock.Setup(m => m.ContentType).Returns(mediaObject.ContentType);
                mock.Setup(m => m.FileName).Returns(System.IO.Path.Combine("C:\\downloads\\", mediaObject.FileName));
                mock.Setup(m => m.InputStream).Returns(new System.IO.MemoryStream(data));

                return mock;
            }

#endif

            #region Jpeg Image

            public static readonly byte[] ImageJpeg =
                new byte[]
                    {
                        255, 216, 255, 224, 0, 16, 74, 70, 73, 70, 0, 1, 2, 0, 0, 100, 0, 100, 0, 0, 255, 236, 0, 17, 68
                        , 117, 99, 107, 121, 0, 1, 0, 4, 0, 0, 0, 80, 0, 0, 255, 238, 0, 38, 65, 100, 111, 98, 101, 0,
                        100, 192, 0, 0, 0, 1, 3, 0, 21, 4, 3, 6, 10, 13, 0, 0, 5, 171, 0, 0, 9, 210, 0, 0, 13, 124, 0, 0
                        , 18, 159, 255, 219, 0, 132, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 2, 2, 2, 3, 4, 3, 2, 2, 3, 4, 5
                        , 4, 4, 4, 4, 4, 5, 6, 5, 5, 5, 5, 5, 5, 6, 6, 7, 7, 8, 7, 7, 6, 9, 9, 10, 10, 9, 9, 12, 12, 12,
                        12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 1, 3, 3, 3, 5, 4, 5, 9, 6, 6, 9, 13, 11, 9, 11,
                        13, 15, 14, 14, 14, 14, 15, 15, 12, 12, 12, 12, 12, 15, 15, 12, 12, 12, 12, 12, 12, 15, 12, 12,
                        12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
                        12, 12, 255, 194, 0, 17, 8, 0, 75, 0, 75, 3, 1, 17, 0, 2, 17, 1, 3, 17, 1, 255, 196, 0, 241, 0,
                        0, 2, 3, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 7, 0, 5, 8, 3, 4, 1, 1, 0, 2, 3, 1, 1, 1, 0,
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 4, 0, 2, 5, 1, 6, 7, 16, 0, 0, 5, 2, 3, 6, 6, 3, 1, 1, 0, 0, 0, 0,
                        0, 0, 0, 1, 2, 3, 4, 5, 6, 17, 18, 19, 16, 33, 49, 65, 34, 21, 50, 66, 20, 52, 37, 69, 53, 22, 7
                        , 51, 54, 17, 0, 1, 2, 4, 2, 4, 7, 8, 17, 5, 1, 0, 0, 0, 0, 0, 1, 2, 3, 0, 17, 18, 4, 33, 19, 16
                        , 49, 20, 5, 65, 81, 97, 177, 34, 50, 21, 129, 161, 114, 51, 69, 133, 165, 197, 113, 145, 193,
                        66, 98, 130, 210, 35, 67, 179, 36, 68, 116, 148, 53, 117, 6, 209, 225, 146, 162, 131, 22, 18, 0,
                        1, 2, 2, 10, 3, 0, 3, 1, 0, 0, 0, 0, 0, 0, 0, 0, 16, 1, 17, 49, 32, 48, 240, 33, 65, 81, 129,
                        161, 193, 241, 113, 2, 18, 64, 97, 50, 66, 19, 1, 0, 1, 3, 2, 5, 4, 3, 1, 1, 0, 0, 0, 0, 0, 0, 1
                        , 17, 0, 33, 49, 16, 65, 81, 97, 113, 129, 177, 240, 145, 161, 193, 32, 209, 225, 241, 48, 255,
                        218, 0, 12, 3, 1, 0, 2, 17, 3, 17, 0, 0, 1, 118, 106, 167, 155, 181, 84, 237, 201, 164, 50, 155,
                        19, 61, 5, 203, 69, 211, 65, 146, 27, 0, 186, 251, 13, 252, 29, 232, 243, 4, 140, 55, 222, 115,
                        78, 220, 246, 20, 110, 128, 140, 87, 91, 182, 20, 195, 203, 201, 27, 41, 156, 64, 195, 21, 53,
                        28, 72, 176, 209, 80, 192, 172, 12, 253, 98, 222, 142, 217, 187, 85, 53, 219, 34, 106, 38, 113,
                        83, 12, 84, 212, 106, 38, 118, 74, 165, 21, 53, 26, 105, 156, 160, 55, 200, 27, 136, 8, 24, 109,
                        68, 207, 123, 200, 32, 113, 217, 82, 214, 85, 239, 151, 188, 183, 165, 190, 200, 20, 113, 145,
                        210, 222, 26, 89, 178, 11, 146, 115, 163, 6, 80, 72, 201, 240, 163, 45, 181, 158, 240, 221, 101,
                        155, 184, 205, 156, 255, 0, 66, 131, 98, 141, 144, 94, 194, 235, 100, 29, 223, 39, 164, 178, 181
                        , 179, 62, 174, 6, 164, 203, 216, 162, 32, 86, 79, 101, 233, 236, 15, 160, 160, 152, 27, 100, 23
                        , 82, 63, 148, 48, 222, 38, 165, 196, 245, 249, 79, 208, 124, 234, 224, 90, 84, 213, 113, 210,
                        147, 236, 84, 246, 144, 76, 13, 194, 169, 85, 239, 34, 199, 13, 201, 66, 208, 163, 25, 192, 7,
                        189, 192, 110, 210, 89, 177, 187, 214, 225, 67, 183, 145, 62, 54, 221, 207, 227, 217, 36, 146,
                        73, 36, 146, 71, 182, 115, 58, 27, 49, 191, 255, 218, 0, 8, 1, 1, 0, 1, 5, 2, 187, 175, 7, 224,
                        62, 170, 245, 113, 70, 205, 94, 224, 144, 236, 107, 78, 228, 91, 72, 164, 92, 61, 242, 228, 77,
                        110, 223, 149, 223, 107, 99, 190, 214, 197, 34, 244, 171, 211, 222, 239, 212, 204, 43, 202, 53,
                        87, 5, 141, 69, 145, 42, 125, 221, 111, 212, 171, 194, 205, 110, 68, 123, 166, 243, 164, 79, 172
                        , 86, 230, 216, 149, 216, 108, 236, 206, 174, 213, 93, 252, 216, 177, 229, 73, 69, 127, 250, 12,
                        249, 208, 158, 176, 156, 113, 219, 138, 252, 174, 84, 32, 61, 97, 214, 38, 84, 162, 93, 113, 91,
                        135, 112, 15, 169, 174, 254, 108, 89, 95, 244, 223, 210, 191, 223, 249, 247, 231, 175, 75, 118,
                        161, 88, 145, 105, 81, 28, 183, 233, 213, 233, 233, 170, 85, 199, 212, 202, 162, 166, 167, 40,
                        237, 154, 161, 28, 42, 61, 126, 157, 38, 165, 14, 231, 171, 157, 62, 153, 113, 82, 159, 82, 46,
                        245, 78, 158, 155, 194, 166, 223, 235, 85, 81, 10, 213, 119, 83, 234, 96, 251, 161, 81, 171, 68,
                        166, 36, 174, 250, 97, 51, 2, 241, 167, 77, 144, 36, 72, 106, 43, 75, 186, 75, 84, 177, 195, 234
                        , 96, 251, 169, 178, 145, 10, 42, 158, 145, 87, 156, 253, 26, 49, 81, 206, 47, 167, 126, 141, 48
                        , 223, 98, 230, 115, 77, 154, 35, 42, 145, 80, 31, 83, 7, 221, 95, 53, 13, 38, 237, 72, 218, 178
                        , 120, 138, 211, 106, 97, 219, 126, 166, 219, 114, 46, 42, 199, 175, 159, 110, 211, 78, 28, 97,
                        245, 51, 147, 217, 107, 21, 59, 75, 188, 79, 166, 82, 162, 82, 153, 19, 232, 241, 42, 10, 43, 26
                        , 154, 66, 13, 163, 76, 133, 32, 78, 156, 150, 75, 244, 249, 62, 158, 185, 135, 108, 95, 106,
                        207, 241, 35, 226, 71, 196, 143, 137, 31, 18, 62, 36, 89, 222, 159, 88, 127, 255, 218, 0, 8, 1,
                        2, 0, 1, 5, 2, 101, 140, 70, 154, 65, 161, 36, 13, 228, 12, 233, 202, 214, 85, 141, 52, 141, 52,
                        133, 176, 147, 26, 102, 27, 240, 137, 11, 192, 153, 112, 144, 31, 222, 134, 22, 73, 74, 100, 36,
                        246, 243, 111, 194, 36, 23, 76, 100, 145, 137, 62, 8, 237, 145, 137, 8, 36, 155, 39, 138, 71,
                        153, 191, 8, 127, 193, 20, 73, 240, 176, 233, 36, 60, 230, 115, 109, 57, 82, 60, 201, 86, 5, 156
                        , 130, 141, 42, 9, 202, 144, 163, 74, 134, 8, 193, 36, 132, 140, 228, 13, 193, 230, 228, 18, 131
                        , 80, 54, 76, 105, 30, 194, 44, 70, 134, 207, 55, 36, 167, 49, 145, 96, 9, 206, 172, 3, 201, 192
                        , 71, 14, 30, 9, 30, 110, 76, 16, 116, 240, 72, 73, 226, 78, 167, 20, 182, 156, 164, 234, 241,
                        217, 230, 65, 230, 74, 93, 202, 22, 179, 86, 196, 184, 105, 30, 160, 193, 186, 103, 176, 136,
                        107, 134, 248, 239, 29, 67, 168, 117, 14, 161, 212, 58, 131, 251, 63, 255, 218, 0, 8, 1, 3, 0, 1
                        , 5, 2, 90, 240, 25, 140, 98, 99, 33, 140, 15, 21, 98, 145, 152, 198, 99, 4, 225, 144, 204, 65,
                        92, 67, 105, 11, 73, 152, 111, 196, 225, 98, 102, 217, 237, 228, 174, 33, 190, 46, 152, 107, 139
                        , 138, 192, 54, 172, 66, 248, 142, 74, 226, 27, 226, 240, 107, 139, 137, 196, 33, 57, 66, 143,
                        19, 28, 140, 134, 80, 88, 144, 60, 76, 22, 36, 58, 129, 230, 49, 148, 101, 28, 182, 56, 241, 32,
                        20, 178, 49, 174, 91, 29, 117, 45, 151, 175, 199, 103, 32, 235, 154, 105, 89, 224, 28, 99, 43,
                        90, 185, 147, 78, 124, 214, 42, 56, 154, 144, 147, 82, 135, 33, 35, 121, 165, 25, 222, 50, 196,
                        54, 222, 69, 70, 45, 55, 94, 86, 161, 196, 99, 46, 206, 74, 220, 122, 65, 182, 137, 27, 29, 140,
                        151, 15, 64, 129, 50, 146, 219, 144, 43, 134, 225, 184, 110, 27, 134, 225, 184, 110, 13, 236,
                        255, 218, 0, 8, 1, 2, 2, 6, 63, 2, 139, 146, 36, 127, 39, 215, 201, 34, 68, 149, 146, 5, 233,
                        125, 13, 120, 25, 94, 41, 23, 46, 25, 53, 224, 100, 113, 210, 242, 226, 9, 175, 3, 120, 72, 57,
                        113, 121, 2, 229, 215, 129, 188, 83, 154, 107, 192, 222, 42, 117, 224, 111, 20, 35, 71, 94, 42,
                        181, 76, 118, 49, 216, 199, 99, 29, 140, 118, 49, 216, 199, 99, 20, 255, 218, 0, 8, 1, 3, 2, 6,
                        63, 2, 89, 144, 137, 58, 113, 252, 27, 103, 85, 108, 234, 173, 157, 9, 44, 92, 146, 91, 52, 137,
                        23, 152, 239, 254, 145, 253, 95, 3, 212, 111, 86, 75, 102, 140, 55, 233, 31, 209, 210, 39, 211,
                        205, 45, 157, 24, 227, 81, 217, 217, 217, 217, 217, 218, 255, 0, 255, 218, 0, 8, 1, 1, 1, 6, 63,
                        2, 94, 235, 221, 146, 77, 194, 0, 218, 110, 245, 210, 78, 52, 167, 150, 9, 59, 226, 247, 30, 39,
                        214, 57, 140, 54, 195, 59, 214, 253, 199, 94, 80, 67, 104, 23, 14, 76, 147, 171, 223, 64, 85,
                        215, 242, 203, 171, 119, 78, 182, 208, 183, 92, 3, 187, 152, 152, 236, 87, 127, 148, 93, 160,
                        155, 109, 165, 55, 8, 113, 213, 97, 85, 50, 164, 173, 60, 240, 197, 183, 254, 142, 250, 239, 57,
                        172, 202, 243, 28, 68, 177, 34, 94, 49, 92, 81, 250, 197, 247, 230, 28, 249, 81, 250, 197, 247,
                        230, 28, 249, 80, 157, 165, 245, 239, 27, 83, 227, 26, 120, 212, 169, 113, 165, 103, 25, 196,
                        243, 240, 216, 123, 66, 114, 250, 9, 202, 126, 204, 248, 35, 124, 19, 143, 219, 95, 30, 210, 200
                        , 208, 198, 247, 10, 107, 101, 177, 117, 73, 117, 181, 19, 89, 86, 94, 18, 18, 150, 179, 199, 22
                        , 72, 178, 184, 101, 166, 88, 172, 186, 219, 170, 80, 154, 140, 164, 122, 41, 84, 92, 218, 93,
                        59, 156, 237, 147, 15, 49, 85, 69, 67, 160, 177, 213, 159, 4, 89, 49, 96, 206, 106, 147, 105, 55
                        , 22, 112, 74, 69, 106, 214, 97, 79, 128, 197, 224, 64, 154, 145, 110, 162, 85, 33, 200, 164,
                        166, 125, 205, 51, 158, 61, 133, 76, 249, 59, 82, 92, 209, 190, 63, 29, 113, 245, 138, 209, 99,
                        106, 139, 135, 19, 108, 233, 116, 187, 110, 20, 66, 20, 67, 42, 196, 167, 81, 213, 27, 176, 89,
                        222, 191, 104, 22, 135, 107, 12, 184, 164, 78, 69, 58, 233, 48, 235, 174, 173, 78, 56, 229, 187,
                        170, 113, 197, 25, 146, 74, 147, 137, 38, 45, 172, 108, 158, 54, 161, 246, 179, 30, 125, 188, 28
                        , 56, 144, 5, 92, 17, 120, 197, 235, 202, 184, 114, 201, 72, 161, 229, 226, 170, 92, 158, 4, 240
                        , 245, 99, 121, 50, 208, 9, 108, 172, 56, 148, 142, 12, 196, 133, 158, 249, 209, 230, 63, 91, 70
                        , 248, 252, 117, 199, 214, 43, 70, 236, 255, 0, 183, 212, 174, 55, 79, 128, 239, 58, 97, 95, 133
                        , 115, 157, 49, 111, 115, 187, 168, 125, 198, 26, 203, 118, 214, 160, 149, 200, 168, 144, 174,
                        148, 132, 92, 57, 126, 164, 54, 253, 193, 204, 184, 196, 73, 180, 32, 96, 10, 181, 97, 140, 95,
                        223, 35, 197, 188, 231, 205, 120, 9, 20, 167, 188, 52, 121, 143, 214, 209, 191, 29, 67, 153, 87,
                        45, 239, 75, 164, 130, 122, 164, 85, 168, 198, 9, 109, 92, 161, 127, 214, 27, 188, 179, 161, 155
                        , 150, 103, 150, 228, 210, 169, 84, 10, 78, 10, 152, 212, 97, 165, 111, 23, 17, 112, 88, 4, 53,
                        226, 211, 41, 235, 234, 129, 27, 77, 130, 144, 195, 244, 148, 87, 52, 43, 3, 200, 160, 99, 180,
                        118, 185, 94, 82, 16, 93, 74, 144, 144, 82, 53, 2, 144, 41, 35, 185, 25, 55, 183, 121, 172, 240,
                        180, 20, 132, 36, 251, 33, 0, 78, 60, 90, 63, 204, 66, 87, 124, 226, 67, 105, 250, 20, 98, 79,
                        178, 99, 204, 126, 182, 141, 255, 0, 251, 181, 215, 56, 208, 11, 234, 37, 197, 117, 25, 78, 42,
                        48, 183, 93, 67, 205, 209, 239, 41, 157, 71, 136, 72, 192, 96, 180, 237, 173, 93, 87, 92, 166,
                        158, 236, 142, 26, 11, 175, 25, 36, 119, 224, 54, 205, 129, 118, 163, 36, 205, 202, 76, 207, 197
                        , 48, 39, 129, 225, 17, 230, 63, 91, 70, 255, 0, 253, 218, 235, 156, 67, 215, 46, 117, 90, 19,
                        238, 194, 150, 165, 77, 110, 156, 79, 16, 131, 100, 150, 132, 219, 69, 85, 112, 212, 53, 153,
                        199, 128, 123, 208, 25, 89, 154, 217, 72, 145, 227, 16, 206, 58, 231, 74, 97, 159, 130, 169, 207
                        , 156, 232, 243, 31, 173, 163, 127, 254, 237, 117, 206, 34, 214, 193, 42, 233, 59, 55, 93, 31, 4
                        , 96, 61, 216, 107, 9, 132, 156, 199, 85, 224, 234, 17, 35, 168, 195, 138, 150, 45, 42, 135, 61,
                        195, 22, 234, 117, 192, 134, 213, 83, 46, 173, 70, 64, 112, 167, 154, 50, 153, 197, 155, 126,
                        131, 60, 164, 235, 49, 156, 240, 249, 247, 198, 174, 36, 255, 0, 125, 30, 99, 245, 180, 94, 166,
                        228, 81, 101, 189, 94, 218, 45, 110, 189, 238, 98, 186, 232, 81, 224, 133, 95, 187, 189, 36, 218
                        , 228, 18, 210, 26, 212, 145, 170, 74, 175, 220, 140, 155, 96, 126, 19, 138, 197, 71, 69, 110,
                        212, 133, 17, 75, 148, 203, 164, 57, 103, 6, 87, 119, 96, 19, 57, 84, 143, 147, 27, 73, 83, 215,
                        110, 12, 82, 31, 41, 32, 30, 57, 0, 52, 108, 236, 125, 162, 253, 254, 133, 181, 170, 49, 81, 81,
                        141, 158, 164, 83, 216, 187, 38, 191, 188, 231, 237, 30, 212, 226, 230, 123, 28, 165, 142, 223,
                        60, 143, 141, 40, 53, 118, 20, 248, 105, 237, 73, 127, 174, 17, 228, 63, 75, 71, 144, 253, 45,
                        30, 67, 244, 180, 121, 15, 210, 209, 228, 63, 75, 71, 144, 253, 45, 14, 236, 253, 143, 78, 95,
                        220, 243, 246, 142, 238, 209, 140, 180, 127, 255, 218, 0, 8, 1, 1, 3, 1, 63, 33, 70, 101, 8, 25,
                        96, 45, 172, 110, 184, 198, 104, 74, 28, 128, 189, 132, 169, 13, 48, 235, 192, 58, 168, 37, 192,
                        217, 147, 11, 191, 208, 171, 142, 9, 221, 180, 80, 76, 206, 234, 248, 49, 107, 170, 231, 118,
                        172, 153, 73, 153, 3, 163, 208, 14, 165, 60, 215, 212, 137, 252, 35, 154, 152, 25, 18, 57, 13,
                        240, 105, 125, 139, 97, 138, 115, 108, 28, 138, 177, 136, 151, 226, 160, 48, 14, 120, 210, 245,
                        69, 130, 193, 69, 22, 233, 75, 20, 249, 67, 112, 122, 37, 236, 78, 198, 93, 169, 92, 16, 239,
                        200, 157, 161, 39, 93, 247, 250, 199, 225, 171, 36, 51, 239, 236, 65, 50, 16, 201, 181, 44, 137,
                        9, 220, 11, 9, 137, 166, 82, 208, 159, 13, 193, 90, 145, 81, 34, 208, 136, 184, 34, 109, 122,
                        153, 226, 238, 82, 138, 239, 5, 93, 189, 232, 216, 135, 16, 66, 129, 180, 47, 252, 35, 44, 230,
                        243, 51, 137, 160, 224, 82, 160, 110, 93, 49, 105, 218, 43, 77, 166, 99, 212, 132, 179, 21, 57,
                        12, 70, 217, 68, 241, 180, 139, 172, 186, 88, 94, 195, 36, 24, 133, 110, 83, 192, 126, 18, 62, 3
                        , 93, 163, 226, 98, 4, 148, 46, 83, 248, 228, 194, 139, 187, 224, 205, 53, 102, 102, 110, 20,
                        133, 216, 218, 165, 155, 170, 1, 200, 180, 139, 195, 75, 166, 17, 106, 219, 145, 30, 237, 44,
                        152, 58, 149, 88, 59, 64, 65, 211, 227, 240, 146, 143, 218, 207, 126, 47, 0, 115, 106, 66, 186,
                        8, 26, 110, 193, 61, 98, 164, 79, 35, 52, 230, 77, 24, 214, 224, 46, 174, 1, 82, 157, 4, 66, 36,
                        4, 30, 90, 192, 44, 88, 102, 30, 186, 201, 91, 57, 17, 199, 0, 247, 166, 229, 51, 183, 182, 7,
                        192, 84, 126, 242, 216, 187, 220, 227, 114, 172, 201, 124, 60, 105, 18, 25, 190, 70, 223, 22,
                        169, 131, 98, 28, 71, 127, 5, 41, 132, 220, 240, 69, 253, 143, 159, 194, 74, 180, 244, 45, 232,
                        215, 225, 83, 239, 184, 135, 185, 20, 128, 66, 66, 18, 145, 23, 63, 73, 191, 221, 11, 78, 185, 4
                        , 145, 94, 40, 42, 13, 176, 227, 49, 166, 234, 227, 149, 15, 139, 152, 206, 104, 242, 246, 214,
                        79, 195, 207, 45, 19, 140, 38, 228, 237, 79, 14, 217, 23, 4, 34, 40, 37, 37, 1, 246, 60, 91, 124
                        , 104, 170, 233, 235, 91, 97, 14, 54, 115, 65, 131, 6, 2, 216, 237, 169, 189, 11, 161, 147, 147,
                        61, 116, 133, 172, 157, 151, 66, 134, 3, 55, 174, 233, 127, 69, 142, 107, 145, 143, 1, 147, 153,
                        210, 55, 174, 114, 255, 0, 79, 244, 126, 114, 73, 39, 123, 110, 120, 251, 62, 122, 127, 255, 218
                        , 0, 8, 1, 2, 3, 1, 63, 33, 35, 183, 92, 183, 181, 5, 40, 246, 169, 86, 31, 31, 170, 108, 195,
                        49, 181, 67, 152, 149, 201, 123, 87, 37, 237, 70, 218, 205, 121, 227, 189, 124, 99, 67, 226, 90,
                        158, 133, 32, 83, 122, 116, 241, 82, 209, 141, 98, 191, 194, 52, 25, 177, 127, 237, 19, 0, 208,
                        139, 56, 209, 109, 202, 48, 238, 169, 115, 171, 248, 70, 158, 183, 90, 198, 158, 122, 45, 40, 71
                        , 178, 162, 53, 62, 206, 105, 9, 177, 167, 21, 20, 217, 45, 73, 72, 210, 225, 104, 252, 45, 49,
                        180, 62, 20, 193, 109, 29, 193, 83, 137, 117, 63, 10, 154, 10, 3, 5, 51, 58, 178, 165, 73, 189,
                        110, 169, 77, 95, 133, 89, 26, 158, 231, 164, 5, 48, 89, 172, 230, 106, 124, 24, 53, 113, 81,
                        146, 156, 64, 82, 183, 208, 200, 49, 71, 4, 162, 163, 77, 199, 21, 9, 158, 174, 209, 21, 135, 61
                        , 179, 71, 231, 219, 109, 185, 69, 253, 209, 31, 26, 127, 255, 218, 0, 8, 1, 3, 3, 1, 63, 33,
                        198, 43, 157, 67, 238, 209, 191, 163, 147, 50, 174, 99, 92, 198, 179, 21, 226, 154, 205, 163, 45
                        , 49, 180, 18, 13, 45, 170, 30, 191, 79, 186, 201, 215, 70, 196, 164, 34, 52, 90, 194, 152, 94,
                        140, 61, 62, 148, 201, 215, 76, 53, 179, 69, 92, 149, 18, 245, 48, 233, 244, 164, 171, 214, 165,
                        65, 36, 173, 253, 11, 133, 76, 166, 150, 74, 149, 28, 85, 244, 166, 239, 93, 51, 121, 225, 79,
                        68, 190, 42, 54, 246, 211, 97, 244, 28, 62, 116, 98, 190, 148, 221, 235, 83, 30, 179, 78, 51,
                        175, 150, 140, 194, 194, 103, 166, 78, 148, 119, 85, 217, 27, 167, 242, 136, 155, 87, 29, 207,
                        141, 62, 148, 221, 235, 66, 39, 11, 210, 23, 107, 189, 168, 4, 52, 219, 43, 21, 115, 112, 158,
                        190, 107, 144, 54, 171, 156, 143, 131, 79, 165, 7, 122, 175, 85, 115, 75, 41, 151, 125, 12, 59,
                        13, 202, 159, 88, 90, 156, 118, 254, 215, 161, 174, 207, 154, 236, 167, 101, 59, 41, 217, 78,
                        202, 118, 82, 28, 189, 117, 211, 255, 218, 0, 12, 3, 1, 0, 2, 17, 3, 17, 0, 0, 16, 51, 184, 191,
                        138, 96, 122, 59, 95, 181, 18, 29, 190, 196, 196, 54, 237, 87, 107, 57, 206, 76, 37, 102, 108,
                        177, 89, 155, 106, 222, 102, 94, 192, 44, 163, 189, 182, 219, 143, 255, 218, 0, 8, 1, 1, 3, 1,
                        63, 16, 141, 126, 198, 36, 13, 136, 147, 55, 64, 4, 139, 148, 167, 177, 242, 29, 138, 108, 181,
                        176, 16, 173, 202, 11, 212, 171, 12, 124, 5, 147, 67, 102, 67, 130, 211, 35, 164, 224, 36, 96,
                        129, 40, 1, 25, 155, 122, 6, 28, 226, 140, 195, 56, 212, 161, 75, 239, 13, 130, 19, 40, 67, 3,
                        59, 147, 102, 248, 227, 110, 96, 152, 238, 24, 102, 155, 200, 154, 189, 185, 236, 6, 134, 31,
                        102, 0, 86, 72, 154, 160, 133, 9, 9, 24, 71, 86, 240, 131, 201, 67, 4, 148, 101, 161, 138, 170,
                        6, 99, 66, 112, 110, 44, 10, 208, 25, 18, 34, 48, 121, 2, 92, 129, 131, 12, 21, 113, 89, 64, 161
                        , 212, 74, 72, 70, 49, 175, 187, 237, 127, 23, 240, 106, 80, 38, 109, 82, 149, 172, 32, 162, 35,
                        129, 68, 92, 237, 40, 176, 22, 20, 78, 38, 152, 19, 199, 177, 89, 81, 85, 89, 90, 155, 31, 204,
                        56, 127, 60, 174, 137, 115, 18, 46, 54, 166, 102, 180, 21, 124, 71, 2, 160, 85, 180, 234, 43, 34
                        , 88, 0, 5, 131, 16, 127, 194, 20, 209, 244, 231, 80, 137, 129, 140, 173, 52, 171, 247, 10, 16,
                        182, 146, 39, 128, 169, 1, 145, 24, 27, 42, 164, 161, 62, 70, 148, 18, 80, 108, 186, 195, 210,
                        93, 5, 148, 93, 50, 1, 203, 35, 104, 52, 131, 2, 43, 237, 224, 169, 92, 249, 7, 129, 176, 132,
                        76, 151, 134, 164, 110, 103, 159, 130, 9, 39, 121, 202, 174, 142, 18, 12, 227, 11, 122, 238, 117
                        , 36, 174, 171, 212, 24, 132, 201, 11, 118, 244, 239, 118, 84, 150, 3, 162, 205, 173, 193, 216,
                        175, 73, 253, 212, 75, 6, 103, 108, 209, 109, 238, 83, 138, 255, 0, 132, 220, 220, 148, 8, 51,
                        120, 164, 237, 33, 202, 105, 38, 193, 157, 128, 72, 161, 118, 112, 40, 251, 4, 104, 73, 130, 210
                        , 44, 18, 200, 110, 133, 233, 34, 205, 60, 84, 192, 202, 138, 78, 37, 65, 223, 173, 34, 198, 181
                        , 134, 19, 47, 37, 18, 67, 80, 196, 32, 184, 64, 144, 119, 141, 102, 230, 12, 150, 157, 239, 150
                        , 137, 229, 83, 130, 211, 184, 216, 192, 16, 39, 17, 106, 129, 224, 140, 150, 90, 21, 18, 198,
                        75, 81, 199, 129, 79, 17, 147, 184, 197, 77, 110, 141, 54, 234, 171, 42, 9, 60, 233, 250, 149,
                        179, 5, 207, 32, 59, 211, 166, 86, 124, 134, 120, 140, 36, 131, 152, 252, 38, 227, 226, 114, 80,
                        144, 181, 152, 78, 15, 20, 35, 124, 108, 50, 243, 106, 127, 1, 197, 9, 9, 89, 129, 92, 96, 89,
                        120, 218, 114, 116, 80, 233, 67, 24, 152, 128, 74, 154, 11, 245, 129, 69, 80, 155, 200, 0, 216,
                        27, 173, 11, 179, 80, 47, 224, 57, 194, 129, 114, 134, 71, 88, 200, 210, 232, 181, 136, 144, 238
                        , 2, 199, 122, 142, 131, 0, 197, 37, 72, 34, 91, 86, 88, 38, 129, 30, 6, 200, 88, 0, 42, 176, 11
                        , 233, 126, 137, 152, 129, 0, 181, 216, 64, 227, 1, 11, 23, 64, 188, 75, 147, 221, 105, 189, 22,
                        235, 23, 82, 179, 56, 147, 150, 133, 160, 147, 247, 32, 211, 217, 48, 176, 197, 121, 20, 92, 183
                        , 241, 111, 21, 251, 30, 162, 227, 227, 157, 108, 94, 190, 245, 2, 59, 79, 103, 231, 36, 146,
                        125, 223, 125, 142, 87, 179, 130, 111, 167, 255, 218, 0, 8, 1, 2, 3, 1, 63, 16, 39, 145, 131,
                        143, 54, 128, 35, 227, 126, 169, 96, 33, 118, 207, 213, 22, 14, 56, 160, 248, 149, 116, 96, 16,
                        109, 57, 134, 154, 109, 204, 96, 126, 138, 255, 0, 45, 250, 175, 242, 223, 170, 176, 166, 210,
                        99, 185, 92, 159, 91, 212, 208, 143, 77, 99, 69, 241, 96, 35, 134, 119, 191, 46, 21, 63, 69, 98,
                        32, 62, 210, 142, 56, 16, 251, 142, 121, 210, 121, 27, 56, 182, 43, 42, 75, 136, 71, 194, 252,
                        235, 11, 125, 50, 175, 86, 224, 104, 215, 136, 68, 49, 114, 205, 234, 204, 34, 100, 30, 60, 104,
                        0, 16, 3, 195, 68, 133, 140, 3, 140, 102, 40, 128, 128, 54, 49, 36, 126, 233, 178, 136, 143, 102
                        , 62, 181, 30, 173, 192, 211, 196, 240, 175, 156, 125, 215, 133, 247, 83, 54, 11, 35, 18, 99, 22
                        , 162, 9, 162, 199, 53, 229, 237, 74, 214, 66, 253, 91, 191, 58, 135, 10, 73, 244, 81, 207, 165,
                        55, 22, 75, 245, 218, 163, 33, 137, 206, 95, 43, 80, 58, 73, 157, 207, 20, 23, 28, 152, 187, 126
                        , 185, 249, 171, 96, 60, 110, 249, 154, 231, 252, 80, 196, 27, 232, 61, 71, 45, 30, 182, 198,
                        238, 41, 88, 159, 119, 242, 164, 152, 49, 177, 157, 35, 252, 180, 20, 65, 219, 252, 167, 65, 234
                        , 57, 80, 3, 191, 141, 235, 7, 3, 212, 208, 118, 217, 99, 179, 106, 38, 154, 51, 197, 229, 64,
                        149, 189, 168, 152, 238, 71, 125, 71, 168, 229, 87, 251, 53, 204, 22, 123, 255, 0, 40, 98, 132,
                        13, 202, 86, 132, 134, 109, 235, 157, 115, 227, 63, 170, 189, 223, 99, 199, 80, 123, 35, 9, 188,
                        24, 104, 89, 109, 198, 164, 24, 152, 12, 26, 90, 195, 192, 208, 127, 167, 238, 149, 88, 79, 15,
                        247, 73, 54, 6, 86, 184, 183, 170, 245, 138, 244, 159, 101, 89, 183, 163, 175, 230, 0, 0, 91,
                        101, 159, 66, 217, 211, 255, 218, 0, 8, 1, 3, 3, 1, 63, 16, 117, 191, 187, 74, 239, 247, 105, 72
                        , 36, 121, 180, 210, 232, 247, 126, 202, 47, 11, 19, 50, 254, 232, 144, 146, 78, 47, 238, 191,
                        222, 107, 253, 230, 158, 186, 78, 117, 204, 253, 20, 167, 174, 249, 208, 221, 144, 63, 85, 18,
                        34, 14, 52, 129, 37, 4, 249, 163, 160, 219, 238, 133, 155, 61, 53, 150, 159, 207, 121, 208, 82,
                        24, 102, 221, 154, 126, 68, 206, 26, 74, 215, 48, 211, 194, 68, 157, 233, 128, 166, 62, 232, 72,
                        122, 155, 234, 190, 123, 206, 158, 119, 134, 178, 238, 175, 19, 71, 46, 193, 138, 117, 115, 115,
                        202, 161, 205, 245, 83, 125, 229, 162, 50, 194, 85, 132, 230, 58, 83, 120, 30, 212, 181, 251,
                        209, 176, 173, 219, 235, 69, 231, 66, 243, 180, 179, 57, 88, 25, 254, 80, 51, 34, 79, 1, 214,
                        255, 0, 85, 8, 90, 119, 216, 121, 240, 161, 154, 80, 176, 61, 215, 129, 206, 152, 39, 135, 19, 7
                        , 195, 230, 154, 133, 33, 141, 23, 157, 70, 161, 46, 3, 138, 176, 123, 231, 149, 47, 73, 30, 230
                        , 3, 151, 130, 141, 131, 35, 11, 204, 16, 232, 146, 15, 55, 171, 241, 179, 230, 142, 116, 176,
                        126, 4, 238, 243, 82, 134, 209, 123, 226, 125, 170, 209, 18, 37, 224, 46, 209, 162, 243, 171,
                        131, 191, 35, 99, 216, 159, 122, 68, 151, 165, 211, 246, 96, 233, 78, 241, 34, 61, 26, 178, 2,
                        158, 160, 254, 67, 67, 142, 112, 188, 179, 224, 84, 153, 198, 221, 28, 123, 191, 17, 80, 133,
                        193, 244, 229, 203, 219, 84, 235, 56, 82, 62, 105, 185, 213, 78, 54, 192, 103, 96, 160, 206, 115
                        , 172, 242, 57, 7, 13, 19, 135, 18, 9, 239, 35, 52, 194, 149, 196, 109, 250, 165, 11, 76, 96,
                        113, 166, 193, 117, 218, 177, 58, 123, 205, 51, 99, 194, 155, 191, 61, 36, 146, 184, 199, 182,
                        103, 71, 255, 217
                    };

            #endregion
        }
    }
}