package com.ecoekb.networkdata.network.data.auth

import com.google.gson.annotations.SerializedName

data class LoginRequest(

    @SerializedName("email")
    var login: String,

    @SerializedName("password")
    var password: String,

    @SerializedName("token")
    var accessToken: String?,

    @SerializedName("refreshToken")
    var refreshToken: String?

)