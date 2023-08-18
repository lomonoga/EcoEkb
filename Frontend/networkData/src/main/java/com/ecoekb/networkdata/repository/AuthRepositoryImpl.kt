package com.ecoekb.networkdata.repository

import com.ecoekb.domain.ApiResponse
import com.ecoekb.domain.models.AuthTokens
import com.ecoekb.domain.models.UserCreditsModel
import com.ecoekb.domain.models.UserRegistrationData
import com.ecoekb.domain.repository.IAuthRepository
import com.ecoekb.networkdata.network.api.AuthApi
import com.ecoekb.networkdata.network.data.auth.LoginRequest
import com.ecoekb.networkdata.network.data.auth.RegistrationRequest
import java.lang.reflect.Executable
import java.net.SocketTimeoutException
import javax.inject.Inject

class AuthRepositoryImpl @Inject() constructor(
    private val authApi: AuthApi
): IAuthRepository {
//    override fun userLogin(userCreditsModel: UserCreditsModel): ApiResponse<AuthTokens> {
//        authApi.login(LoginRequest())
//        if (userCreditsModel.email == "root" && userCreditsModel.password == "root"){
//            return ApiResponse.Success<AuthTokens>(AuthTokens("11", "22"))
//        } else {
//            return ApiResponse.Error<AuthTokens>(Exception())
//        }
//    }

    override suspend fun userLogin(userCreditsModel: UserCreditsModel) : ApiResponse<AuthTokens> {
        try {
            val loginRequest = LoginRequest(
                userCreditsModel.email,
                userCreditsModel.password,
                null,
                null)
            val response = authApi.login(loginRequest)

            if (response.code() == 200) {
                val responseToken = response.body()!!
                val authToken = AuthTokens(responseToken.accessToken, responseToken.refreshToken)
                return ApiResponse.Success(data = authToken)
            } else if (response.code() == 401) {
                return ApiResponse.Error(Exception())
            }
            throw RuntimeException()
        } catch (e: RuntimeException) {
            return ApiResponse.Error(e)
        } catch (e: SocketTimeoutException) {
            return ApiResponse.Error(e)
        }
    }

//    override suspend fun userRegistration(userRegistrationData: UserRegistrationData): ApiResponse<Boolean> {
//        if (userRegistrationData.firstName == "A"
//            && userRegistrationData.secondName == "B"
//            && userRegistrationData.surName == "C"
//            && userRegistrationData.email == "email"
//            && userRegistrationData.password == "1") {
//            return ApiResponse.Success<Boolean>(true)
//        } else {
//            return ApiResponse.Error<Boolean>(Exception())
//        }
//    }

    override suspend fun userRegistration(userRegistrationData: UserRegistrationData): ApiResponse<Boolean> {
        try {
            val loginRequest = RegistrationRequest(
                fullName = "${userRegistrationData.firstName}\n" +
                        "${userRegistrationData.secondName}\n" +
                        "${userRegistrationData.surName}",
                email = userRegistrationData.email,
                password = userRegistrationData.password
            )
            val response = authApi.registration(loginRequest)

            if (response.code() == 200) {
                return ApiResponse.Success(data = true)
            } else if (response.code() == 401) {
                return ApiResponse.Error(Exception())
            }
            throw RuntimeException()
        } catch (e: RuntimeException) {
            return ApiResponse.Error(e)
        } catch (e: SocketTimeoutException) {
            return ApiResponse.Error(e)
        }
    }

}