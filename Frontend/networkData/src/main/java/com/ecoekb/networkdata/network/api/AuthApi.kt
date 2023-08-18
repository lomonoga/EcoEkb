package com.ecoekb.networkdata.network.api

import com.ecoekb.networkdata.network.Urls
import com.ecoekb.networkdata.network.data.auth.GetAuthToken
import com.ecoekb.networkdata.network.data.auth.LoginRequest
import com.ecoekb.networkdata.network.data.auth.RegistrationRequest
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.POST

interface AuthApi {

    @POST(Urls.REGISTRATION)
    suspend fun registration(@Body request: RegistrationRequest) : Response<Unit>

    @POST(Urls.GET_ACCESS_TOKEN)
    suspend fun login(@Body request: LoginRequest) : Response<GetAuthToken>

}