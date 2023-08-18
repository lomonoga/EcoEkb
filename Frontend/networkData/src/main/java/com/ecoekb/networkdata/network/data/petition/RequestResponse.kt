package com.ecoekb.networkdata.network.data.petition

import com.google.gson.annotations.SerializedName

data class RequestResponse (
    @SerializedName("id")
    var id: String,
    @SerializedName("refreshToken")
    var refreshToken: String,
    @SerializedName("expiresAt")
    var expiresAt: String
)

//"id": "4337c98b-7313-480e-b2e9-b209d87180f3",
//"description": "Незаконно торгуют апельсинами!",
//"topic": "UnauthorizedTrading",
//"address": "Розы Люксембург, 56А",
//"companyName": "ТЕХНОХАБ СБЕРА",
//"status": "New"