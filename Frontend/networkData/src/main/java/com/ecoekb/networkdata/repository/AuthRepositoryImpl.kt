package com.ecoekb.networkdata.repository

import com.ecoekb.domain.ApiResponse
import com.ecoekb.domain.models.AuthTokens
import com.ecoekb.domain.models.UserCreditsModel
import com.ecoekb.domain.models.UserRegistrationData
import com.ecoekb.domain.repository.IAuthRepository

class AuthRepositoryImpl : IAuthRepository {
    override fun userLogin(userCreditsModel: UserCreditsModel): ApiResponse<AuthTokens> {
        if (userCreditsModel.email == "email" && userCreditsModel.password == "1"){
            return ApiResponse.Success<AuthTokens>(AuthTokens("11", "22"))
        } else {
            return ApiResponse.Error<AuthTokens>(Exception())
        }
    }

    override fun userRegistration(userRegistrationData: UserRegistrationData): ApiResponse<Boolean> {
        if (userRegistrationData.firstName == "A"
            && userRegistrationData.secondName == "B"
            && userRegistrationData.surName == "C"
            && userRegistrationData.email == "email"
            && userRegistrationData.password == "1") {
            return ApiResponse.Success<Boolean>(true)
        } else {
            return ApiResponse.Error<Boolean>(Exception())
        }    }
}