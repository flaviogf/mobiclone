import 'package:equatable/equatable.dart';

abstract class PasswordEvent extends Equatable {}

class PasswordEventSubmit extends PasswordEvent {
  final String value;

  PasswordEventSubmit(this.value);

  @override
  List<Object> get props => [];
}
