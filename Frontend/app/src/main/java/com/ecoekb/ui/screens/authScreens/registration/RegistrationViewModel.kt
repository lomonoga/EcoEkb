package com.ecoekb.ui.screens.authScreens.registration

import androidx.compose.runtime.MutableState
import androidx.compose.runtime.mutableStateOf
import androidx.lifecycle.ViewModel
import com.ecoekb.domain.models.UserRegistrationData
import com.ecoekb.domain.usecase.UserUsecase
import dagger.hilt.android.lifecycle.HiltViewModel
import javax.inject.Inject

@HiltViewModel
class RegistrationViewModel @Inject() constructor(
    private val useCase: UserUsecase
) : ViewModel() {

    var firstName: MutableState<String> = mutableStateOf("")
    var secondName: MutableState<String> = mutableStateOf("")
    var surName: MutableState<String> = mutableStateOf("")
    var email: MutableState<String> = mutableStateOf("")
    var password: MutableState<String> = mutableStateOf("")
    var passwordDuplicate: MutableState<String> = mutableStateOf("")

    var correctFirstName: MutableState<Boolean> = mutableStateOf(false)
    var correctSecondName: MutableState<Boolean> = mutableStateOf(false)
    var correctEmail: MutableState<Boolean> = mutableStateOf(false)
    var correctPassword: MutableState<Boolean> = mutableStateOf(false)
    var correctPasswordDuplicate: MutableState<Boolean> = mutableStateOf(false)

    fun checkCorrectFullName() : Boolean {
        correctFirstName.value = firstName.value == ""
        correctSecondName.value = secondName.value == ""
        return !correctFirstName.value && !correctSecondName.value
    }

    fun checkCorrectPassword() : Boolean {
        correctEmail.value = email.value == ""
        correctPassword.value = password.value == ""
        correctPasswordDuplicate.value = passwordDuplicate.value == ""
        return (!correctEmail.value && !correctPassword.value && !correctPasswordDuplicate.value)
    }

    fun checkEqualsPassword() : Boolean {
        return if (password.value == passwordDuplicate.value) {
            useCase.registrationUser(UserRegistrationData(
                firstName = firstName.value,
                secondName = secondName.value,
                surName = surName.value,
                email = email.value,
                password = password.value
            ))
            true
        } else {
            false
        }
    }


}