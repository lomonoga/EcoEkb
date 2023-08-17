package com.ecoekb.domain.models

data class AuthTokens(
    val accessToken: String,
    val refreshToken: String
) {}