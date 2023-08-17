package com.ecoekb.domain.models

data class UserRegistrationModel(
    val firstName: String,
    val secondName: String,
    val surName: String,
    val email: String,
    val password: String
) {}