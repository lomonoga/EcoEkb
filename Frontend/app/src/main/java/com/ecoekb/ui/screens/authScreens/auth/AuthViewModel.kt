package com.ecoekb.ui.screens.authScreens.auth

import android.util.Log
import androidx.compose.runtime.MutableState
import androidx.compose.runtime.mutableStateOf
import androidx.lifecycle.ViewModel
import com.ecoekb.domain.models.AuthTokens
import com.ecoekb.domain.models.UserCreditsModel
import com.ecoekb.domain.usecase.UserUsecase
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.runBlocking
import javax.inject.Inject

@HiltViewModel
class AuthViewModel @Inject constructor(
    private val useCase: UserUsecase
) : ViewModel() {

    val email = mutableStateOf("")
    val password = mutableStateOf("")

    val correctEmail = mutableStateOf(false)
    val correctPassword  = mutableStateOf(false)

    val status: MutableState<Status> = mutableStateOf(Status.SUCCESS)

    val loginCorrect = mutableStateOf(false)

    fun checkCorrectCredits() : Boolean {
        correctEmail.value = email.value == ""
        correctPassword.value = password.value == ""
        return !correctEmail.value && !correctPassword.value
    }

    fun loginUserByCredits() {
        status.value = Status.WAIT
        runBlocking {
            val authTokens: AuthTokens? = useCase.loginUserByCredits(
                UserCreditsModel(email.value, password.value)
            )

            if (authTokens == null) {
                status.value = Status.ERROR
                loginCorrect.value = false
            } else {
                status.value = Status.SUCCESS
                loginCorrect.value = true
            }
        }
    }
}


enum class Status {
    SUCCESS,
    ERROR,
    WAIT;
}