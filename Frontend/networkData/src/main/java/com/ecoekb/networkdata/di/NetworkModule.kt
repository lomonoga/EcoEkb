package com.ecoekb.networkdata.di

import android.content.Context
import com.ecoekb.domain.repository.IAuthRepository
import com.ecoekb.networkdata.repository.AuthRepositoryImpl
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.qualifiers.ApplicationContext
import dagger.hilt.components.SingletonComponent
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
class NetworkModel {

    @Singleton
    @Provides
    fun provideAuthRepository(@ApplicationContext context: Context): IAuthRepository {
        return AuthRepositoryImpl()
    }

}