﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.510
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 1.1.4322.510.
// 
namespace CFMap.MPService {
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="MapPointSoapBinding", Namespace="http://128.83.129.226:8080/axis/services/MapPoint")]
    public class MPClientService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public MPClientService() {
            this.Url = "http://128.83.129.226:8080/axis/services/MapPoint";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://128.83.129.226:8080/axis/services/MapPoint", ResponseNamespace="http://128.83.129.226:8080/axis/services/MapPoint")]
        [return: System.Xml.Serialization.SoapElementAttribute("getMapReturn", DataType="base64Binary")]
        public System.Byte[] getMap(int in0, int in1, int in2) {
            object[] results = this.Invoke("getMap", new object[] {
                        in0,
                        in1,
                        in2});
            return ((System.Byte[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BegingetMap(int in0, int in1, int in2, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("getMap", new object[] {
                        in0,
                        in1,
                        in2}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Byte[] EndgetMap(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Byte[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://128.83.129.226:8080/axis/services/MapPoint", ResponseNamespace="http://128.83.129.226:8080/axis/services/MapPoint")]
        [return: System.Xml.Serialization.SoapElementAttribute("getDirectionsReturn")]
        public string[] getDirections(string in0, string in1, string in2, string in3, string in4, string in5, string in6, string in7) {
            object[] results = this.Invoke("getDirections", new object[] {
                        in0,
                        in1,
                        in2,
                        in3,
                        in4,
                        in5,
                        in6,
                        in7});
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BegingetDirections(string in0, string in1, string in2, string in3, string in4, string in5, string in6, string in7, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("getDirections", new object[] {
                        in0,
                        in1,
                        in2,
                        in3,
                        in4,
                        in5,
                        in6,
                        in7}, callback, asyncState);
        }
        
        /// <remarks/>
        public string[] EndgetDirections(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://128.83.129.226:8080/axis/services/MapPoint", ResponseNamespace="http://128.83.129.226:8080/axis/services/MapPoint")]
        [return: System.Xml.Serialization.SoapElementAttribute("getSegmentNumReturn")]
        public int getSegmentNum() {
            object[] results = this.Invoke("getSegmentNum", new object[0]);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BegingetSegmentNum(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("getSegmentNum", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public int EndgetSegmentNum(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((int)(results[0]));
        }
    }
}