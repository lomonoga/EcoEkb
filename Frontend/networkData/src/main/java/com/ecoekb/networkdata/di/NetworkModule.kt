package com.ecoekb.networkdata.di

import android.content.Context
import com.ecoekb.domain.repository.IAuthRepository
import com.ecoekb.domain.repository.IRequestRepository
import com.ecoekb.networkdata.network.NetworkClient
import com.ecoekb.networkdata.network.api.AuthApi
import com.ecoekb.networkdata.repository.AuthRepositoryImpl
import com.ecoekb.networkdata.repository.RequestRepositoryImpl
import com.ecoekb.networkdata.utils.TokenDataStore
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.qualifiers.ApplicationContext
import dagger.hilt.components.SingletonComponent
import retrofit2.Retrofit
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
class NetworkModel {

    @Singleton
    @Provides
    fun provideDataStoreToken(@ApplicationContext context: Context): TokenDataStore {
        return TokenDataStore(context)
    }

    @Singleton
    @Provides
    fun provideMainRetrofit(tokenManager: TokenDataStore) : Retrofit {
        return NetworkClient.getMainRetrofit(tokenManager)
    }

    @Singleton
    @Provides
    fun provideAuthApi() : AuthApi {
        return NetworkClient.getAuthApi()
    }

//    @Singleton
//    @Provides
//    fun provideUserApi(mainRetrofit: Retrofit) : UserApi {
//        return NetworkClient.getUserApi(mainRetrofit)
//    }
//
//
//
//    @Singleton
//    @Provides
//    fun provideUserRepository(authApi: AuthApi, userApi: UserApi): IUserNetworkRepository {
//        return UserNetworkRepositoryImpl(authApi, userApi)
//    }

    @Singleton
    @Provides
    fun provideAuthRepository(@ApplicationContext context: Context, authApi: AuthApi): IAuthRepository {
        return AuthRepositoryImpl(authApi)
    }

    @Singleton
    @Provides
    fun provideRequestRepository(@ApplicationContext context: Context): IRequestRepository {
        return RequestRepositoryImpl()
    }

}