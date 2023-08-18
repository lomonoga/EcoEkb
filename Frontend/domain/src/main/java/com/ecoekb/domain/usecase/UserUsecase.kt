package com.ecoekb.domain.usecase

import com.ecoekb.domain.ApiResponse
import com.ecoekb.domain.models.AuthTokens
import com.ecoekb.domain.models.UserCreditsModel
import com.ecoekb.domain.models.UserRegistrationData
import com.ecoekb.domain.repository.IAuthRepository
import javax.inject.Inject

class UserUsecase @Inject constructor(
    private val repository: IAuthRepository
) {
    suspend fun loginUserByCredits(userCredits: UserCreditsModel) : AuthTokens? {
        val response: ApiResponse<AuthTokens> = repository.userLogin(userCredits)

        return if (response is ApiResponse.Success) {
            response.data
        } else {
            null
        }
    }

    suspend fun registrationUser(userRegistrationData: UserRegistrationData) : Boolean {
        val response: ApiResponse<Boolean> = repository.userRegistration(userRegistrationData)

        return response is ApiResponse.Success
    }
}