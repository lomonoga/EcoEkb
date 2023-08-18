package com.ecoekb.networkdata.network.data.auth

import com.google.gson.annotations.SerializedName

data class RegistrationRequest(

    @SerializedName("fullName")
    var fullName: String,

    @SerializedName("email")
    var email: String,

    @SerializedName("password")
    var password: String
) {}