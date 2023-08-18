package com.ecoekb.networkdata.network

import com.ecoekb.networkdata.network.api.AuthApi
import com.ecoekb.networkdata.utils.TokenDataStore
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class NetworkClient {

    companion object {
        private const val baseUrl = Urls.BASE_URL

        fun getAuthApi(): AuthApi {
            return getAuthRetrofit().create(AuthApi::class.java)
        }

        private fun getAuthRetrofit() : Retrofit {
            val logInterceptor = HttpLoggingInterceptor()
            logInterceptor.level = HttpLoggingInterceptor.Level.BODY

            val client = OkHttpClient()
                .newBuilder()
                .addInterceptor(logInterceptor)
                .build()

            return createRetrofit(client)
        }

        fun getMainRetrofit(tokenManager: TokenDataStore) : Retrofit {
            setTokens(tokenManager)

            val logInterceptor = HttpLoggingInterceptor()
            logInterceptor.level = HttpLoggingInterceptor.Level.BODY

            val client = OkHttpClient()
                .newBuilder()
                .addInterceptor(AuthInterceptor())
                .addInterceptor(logInterceptor)
                .build()

            return createRetrofit(client)
        }

        private fun createRetrofit(client: OkHttpClient): Retrofit {
            return Retrofit.Builder()
                .baseUrl(baseUrl)
                .client(client)
                .addConverterFactory(GsonConverterFactory.create())
                .build()
        }

        private fun setTokens(tokenManager: TokenDataStore){
            AuthInterceptor().setTokenManager(tokenManager)
        }

//        fun getPetitionApi(retrofit: Retrofit): PetitionApi {
//            return retrofit.create(PetitionApi::class.java)
//        }
    }
}