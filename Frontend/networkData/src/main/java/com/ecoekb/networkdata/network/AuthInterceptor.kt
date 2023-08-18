package com.ecoekb.networkdata.network

import android.annotation.SuppressLint
import com.ecoekb.domain.models.AuthTokens
import com.ecoekb.networkdata.utils.TokenDataStore
import kotlinx.coroutines.flow.first
import kotlinx.coroutines.runBlocking
import okhttp3.Interceptor
import okhttp3.Response

class AuthInterceptor : Interceptor {

    companion object {
        private const val authHeader = "Authorization"
        private const val scheme = "Bearer "
        private lateinit var token: AuthTokens

        @SuppressLint("StaticFieldLeak")
        private lateinit var tokenManager: TokenDataStore
    }

    fun setTokenManager(tokenManagerNew: TokenDataStore){
        tokenManager = tokenManagerNew
    }

    private fun setTokens(){
        token = runBlocking {
            tokenManager.getTokens().first()
        }
    }

    override fun intercept(chain: Interceptor.Chain): Response {
        setTokens()

        val originalRequest = chain.request()
        val builder = originalRequest
            .newBuilder()
            .header(authHeader, scheme + token.accessToken)
        val newRequest = builder.build()
        return chain.proceed(newRequest)
    }
}