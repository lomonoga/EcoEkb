package com.ecoekb.networkdata.network.data.auth

import com.google.gson.annotations.SerializedName

data class GetAuthToken (

    @SerializedName("token")
    var accessToken: String,
    @SerializedName("refreshToken")
    var refreshToken: String,
    @SerializedName("expiresAt")
    var expiresAt: String
){}
