package com.ecoekb.domain.repository

import com.ecoekb.domain.ApiResponse
import com.ecoekb.domain.models.AuthTokens
import com.ecoekb.domain.models.UserCreditsModel
import com.ecoekb.domain.models.UserRegistrationData

interface IAuthRepository {

    fun userLogin(userCreditsModel: UserCreditsModel) : ApiResponse<AuthTokens>

    fun userRegistration(userRegistrationData: UserRegistrationData) : ApiResponse<Boolean>
}